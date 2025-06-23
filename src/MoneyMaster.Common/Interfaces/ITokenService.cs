using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string id, string email);
        string GenerateRefreshToken();

    }
}
