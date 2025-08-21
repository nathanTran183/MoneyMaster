using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IUserRefreshTokenRepository
    {
        Task<IEnumerable<UserRefreshToken>> GetUserRefreshTokensByUserIdAsync(string userId);
        UserRefreshToken? GetUserRefreshTokenByToken(string token);
        Task RevokeUserRefreshToken(int id);
        Task DeleteUserRefreshToken(UserRefreshToken tokenObj);
        Task<int> AddUserRefreshTokenAsync(UserRefreshToken tokenObj);
        Task<bool> IsRefreshTokenValidAsync(string token);
    }
}
