using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common.Models.Responses;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        Task<ServiceResult<RegisterResponse>> RegisterUserAsync(RegisterRequest loginRequest);
    }
}
