using MoneyMaster.Common.DTOs;
using System;

namespace MoneyMaster.Common.Models.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public UserDTO User { get; set; }
}
