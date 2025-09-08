using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyMaster.Common.Entities;
using MoneyMaster.Common.Utilities.Exceptions;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoneyMaster.Service.Services;

public class JwtTokenService : ITokenService
{
    readonly string jwtKey;
    readonly string jwtRefreshKey;
    readonly string jwtIssuer;
    readonly string jwtAudience;
    readonly IConfiguration configuration;
    UserManager<User> userManager;
    IUserRefreshTokenRepository tokenRepository;

    public JwtTokenService(IConfiguration configuration, UserManager<User> userManager, IUserRefreshTokenRepository tokenRepository)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.tokenRepository = tokenRepository;

        jwtKey = configuration["Jwt:Key"] ?? string.Empty;
        jwtRefreshKey = configuration["Jwt:RefreshTokenSecretKey"] ?? string.Empty;
        jwtIssuer = configuration["Jwt:Issuer"] ?? string.Empty;
        jwtAudience = configuration["Jwt:Audience"] ?? string.Empty;
    }

    public async Task<(string accessToken, string refreshToken)> GenerateTokenAsync(string userId, string email, IEnumerable<string> userRoles)
    {
        var accessToken = GenerateAccessToken(userId, email, userRoles);
        var refreshToken = GenerateRefreshToken();

        await StoreRefreshTokenAsync(userId, refreshToken);

        return (accessToken, refreshToken);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var principal = ValidateToken(refreshToken, true);
        if (principal == null)
        {
            throw new Exception("Fail to validate refresh token");
        }
        if (!principal.Claims.Any(c => c.Type == "token_type" && c.Value == "refresh"))
        {
            throw new NotRefreshTokenTypeException("Token is not refresh token type");
        }

        var userRefreshToken = tokenRepository.GetUserRefreshTokenByToken(refreshToken);
        if (userRefreshToken == null)
        {
            throw new InvalidRefreshTokenException("Refresh token does not exist");
        }
        
        if (!await tokenRepository.IsRefreshTokenValidAsync(refreshToken))
        {
            throw new InvalidRefreshTokenException("Invalid refresh token");
        }

        var user = await userManager.FindByIdAsync(userRefreshToken.UserId);
        if (user == null || !user.IsActive)
        {
            throw new InvalidUserException("Invalid user or not existed");
        }

        await RevokeRefreshTokenAsync(refreshToken);
        var userRoles = await userManager.GetRolesAsync(user);
        return await GenerateTokenAsync(userRefreshToken.UserId, user.Email!, userRoles);
    }

    public Task RevokeRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? ValidateToken(string token, bool isRefreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(isRefreshToken ? jwtRefreshKey : jwtKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtAudience,
                ValidateLifetime = true, // This checks the embedded expiration
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private string GenerateAccessToken(string userId, string email, IEnumerable<string> userRoles)
    {
        if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
        {
            throw new InvalidOperationException("JWT configuration is missing");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:AccessTokenExpirationMinutes"] ?? "15")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        if (string.IsNullOrWhiteSpace(jwtRefreshKey) || string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
        {
            throw new InvalidOperationException("JWT configuration is missing");
        }

        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtRefreshKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim> 
        {
            new("token_type", "refresh"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("random", Convert.ToBase64String(randomBytes))
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["Jwt:RefreshTokenExpirationDays"] ?? "15")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task StoreRefreshTokenAsync(string userId, string refreshToken)
    {
        var tokenObj = new UserRefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["Jwt:RefreshTokenExpirationDays"] ?? "15")),
        };
        await tokenRepository.AddUserRefreshTokenAsync(tokenObj);
    }
}
