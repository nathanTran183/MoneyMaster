using MoneyMaster.Database.Entities;
using System;

namespace MoneyMaster.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        DateTime GetExpirationDate();
    }
}
