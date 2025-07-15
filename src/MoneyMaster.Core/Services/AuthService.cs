using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using MoneyMaster.Common;
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
        readonly ITokenService tokenService;
        readonly IUserRepository userRepository;

        public AuthService(UserManager<User> userManager, ITokenService tokenService, IUserRepository userRepository)
        {
            this.userManager = userManager;
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

            var user = await userManager.FindByEmailAsync(registerRequest.Email);
            if (user != null) 
            {
                result.AddErrors($"{registerRequest.Email} is already registered.");
                return result;
            }

            user = new User
            {
                Email = registerRequest.Email,
                PasswordHash = registerRequest.Password
            };
            var userRes = await userManager.CreateAsync(user);
            if (!userRes.Succeeded)
            {
                foreach (var error in userRes.Errors)
                {
                    result.AddErrors(error.Description);
                }
                return result;
            }

            await userManager.AddToRoleAsync(user, Constants.UserRole);
            
            var token = tokenService.GenerateAccessToken(user.Id, user.Email!, [Constants.UserRole]);
            var refreshToken = tokenService.GenerateAccessToken(user.Id, user.Email!, [Constants.UserRole], true);
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
