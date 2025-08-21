using System;

namespace MoneyMaster.Common.DTOs
{
    public class BaseDTO
    {
        public int Id {  get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }

        public UserDTO User { get; set; }
    }
}
