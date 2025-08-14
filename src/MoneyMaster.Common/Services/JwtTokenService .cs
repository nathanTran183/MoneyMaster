using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyMaster.Common.Interfaces;
using MoneyMaster.Common.Utilities.Exceptions;
using MoneyMaster.Database.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMaster.Common.Services
{
    public class JwtTokenService : ITokenService
    {
        readonly string jwtKey;
        readonly string jwtRefreshKey;
        readonly string jwtIssuer;
        readonly string jwtAudience;
        readonly IConfiguration configuration;
        UserManager<User> userManager;

        public JwtTokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;

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
            var principal = ValidateToken(refreshToken,true);

            if (!principal.Claims.Any(c => c.Type == "token_type" && c.Value == "refresh"))
                throw new NotRefreshTokenTypeException("Token is not refresh token type");

            var userRefreshToken = repository.GetUserRefreshTokenByToken(refreshToken);
            if (userRefreshToken == null)
            {
                throw new InvalidRefreshTokenException("Invalid refresh token"); 
            }
            // Check if refresh token is still valid in database
            //if (!await IsRefreshTokenValidAsync(refreshToken))
            //    throw new InvalidRefreshTokenException("") 

            var user = await userManager.FindByIdAsync(userRefreshToken.UserId);
            if (user == null || !user.IsActive)
                throw new InvalidUserException("Invalid user or not existed");

            // Revoke old refresh token
            await RevokeRefreshTokenAsync(refreshToken);

            // Generate new tokens
            return await GenerateTokenAsync(userRefreshToken.UserId, user.);
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
            // Implement database logic to store refresh token
            // You'll need a RefreshToken table or add it to ApplicationUser
            await Task.CompletedTask; // Placeholder
        }
    }
}
