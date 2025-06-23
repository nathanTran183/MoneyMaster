using Microsoft.AspNetCore.Identity;
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
        readonly UserManager<User> userManager;
        readonly IPasswordHasher passwordHasher;
        readonly ITokenService tokenService;
        readonly IUserRepository userRepository;

        public AuthService(UserManager<User> userManager, IPasswordHasher passwordHasher, ITokenService tokenService, IUserRepository userRepository)
        {
            this.userManager = userManager;
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

        public async Task<ServiceResult<RegisterResponse>> RegisterUserAsync(RegisterRequest registerRequest)
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

            // TODO - Create Roles and UserRoles, maybe use the AspNetUserTokens to store the refreshToken
            
            var token = tokenService.GenerateAccessToken(user.Id, user.Email!);
            var refreshToken = tokenService.GenerateRefreshToken();
            var res = new RegisterResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(3600)
            };

            result.Value = res;
            return result;
        }
    }
}
