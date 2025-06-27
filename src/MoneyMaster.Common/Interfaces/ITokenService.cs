using System.Collections.Generic;

namespace MoneyMaster.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string id, string email, IEnumerable<string> userRoles, bool isRefreshToken = false);

    }
}
