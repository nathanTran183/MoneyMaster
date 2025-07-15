using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyMaster.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(string accessToken, string refreshToken)> GenerateTokenAsync(string id, string email, IEnumerable<string> userRoles);
        Task RevokeRefreshTokenAsync(string refreshToken);
        Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
