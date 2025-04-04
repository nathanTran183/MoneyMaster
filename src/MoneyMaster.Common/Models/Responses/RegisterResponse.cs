using MoneyMaster.Common.DTOs;
using System;

namespace MoneyMaster.Common.Models.Responses;

public class RegisterResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public UserDTO User { get; set; }
}
