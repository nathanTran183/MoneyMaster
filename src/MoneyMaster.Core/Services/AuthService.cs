using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common.Interfaces;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
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

        public Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest)
        {
            //var result = new ServiceResult() { Success = true };

            //var email = userRepository.GetUserByEmailAsync(registerRequest.Email);
            //if (email != null)
            //{
                
            //}
            throw new NotImplementedException();
        }
    }
}
