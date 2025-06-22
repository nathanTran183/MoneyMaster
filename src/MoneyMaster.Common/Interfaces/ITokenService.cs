using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(RegisterDTO user);
    }
}
