using System;
using MoneyMaster.Core.DTOs;

namespace MoneyMaster.Common.Models.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public UserDTO User { get; set; }
}
