using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common.Models.Responses;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<RegisterResponse> LoginAsync(RegisterRequest loginRequest);
    }
}
