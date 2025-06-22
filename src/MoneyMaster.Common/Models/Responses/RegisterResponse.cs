using System;

namespace MoneyMaster.Common.Models.Responses;

public class RegisterResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string UserId { get; set; }
}
