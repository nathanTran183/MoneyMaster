using MoneyMaster.Database.Entities;
using System;

namespace MoneyMaster.Common.Entities
{
    public class UserRefreshToken : BaseEntity
    {
        public string Token { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsActive { get; set; }
    }
}
