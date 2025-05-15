using MoneyMaster.Common.Interfaces;
using MoneyMaster.Database.Entities;
using System;

namespace MoneyMaster.Common.Services
{
    public class JwtTokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var token = "";
            return token;
        }

        public DateTime GetExpirationDate()
        {
            return DateTime.Now;
        }
    }
}
