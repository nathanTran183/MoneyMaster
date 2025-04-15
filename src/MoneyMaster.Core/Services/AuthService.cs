using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            //var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            //if (user == null)
            //{
            //    throw new UnauthorizedException("Invalid credentials");
            //}
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> LoginAsync(RegisterRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
