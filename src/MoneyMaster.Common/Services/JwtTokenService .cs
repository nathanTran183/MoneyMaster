using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyMaster.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoneyMaster.Common.Services
{
    public class JwtTokenService : ITokenService
    {
        readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(string id, string email, IEnumerable<string> userRoles, bool isRefreshToken = false)
        {
            var jwtKey = isRefreshToken ? configuration["Jwt:RefreshTokenSecretKey"] : configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
            {
                throw new InvalidOperationException("JWT configuration is missing");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();

            if (isRefreshToken)
            {
                claims =
                [
                    new(ClaimTypes.NameIdentifier, id),
                    new(JwtRegisteredClaimNames.Sub, id),
                    new(JwtRegisteredClaimNames.Email, email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            else
            {
                var randomBytes = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomBytes);

                claims =
                [
                    new("token_type", "refresh"),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new("random", Convert.ToBase64String(randomBytes))
                ];
            }

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: isRefreshToken ?
                    DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["Jwt:RefreshTokenExpirationDays"] ?? "7")) :
                    DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:AccessTokenExpirationMinutes"] ?? "15")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
