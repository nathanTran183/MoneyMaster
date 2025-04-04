namespace MoneyMaster.Common.DTOs;

public class UserDTO
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Avatar { get; set; }
}
