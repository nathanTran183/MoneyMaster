using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common.Interfaces;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IUserRepository userRepository;

        public AuthService(IPasswordHasher passwordHasher, ITokenService tokenService, IUserRepository userRepository)
        {
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
            this.userRepository = userRepository;
        }

        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            //var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            //if (user == null)
            //{
            //    throw new UnauthorizedException("Invalid credentials");
            //}
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest)
        {
            var result = new ServiceResult<RegisterResponse>();

            if (await userRepository.IsEmailExistAsync(registerRequest.Email)) 
            {
                result.AddErrors($"{registerRequest.Email} is already registered.");
            }

            var user = new User
            {
                Email = registerRequest.Email,
                PasswordHash = passwordHasher.HashPassword(registerRequest.Password),
            };
            user = await userRepository.SaveUserAsync(user);
            
            var token = tokenService.GenerateToken(user);
            var res = new RegisterResponse
            {
                Token = token,
                RefreshToken = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(3600)
            };

            result.Value = res;
            return result;
        }
    }
}
