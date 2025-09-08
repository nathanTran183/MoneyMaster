using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories;

public class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    MoneyMasterContext context;
    public UserRefreshTokenRepository(MoneyMasterContext context)
    {
        this.context = context;
    }

    public UserRefreshToken? GetUserRefreshTokenByToken(string token)
    {
        return context.UserRefreshTokens.SingleOrDefault(t => t.Token == token);
    }

    public async Task<IEnumerable<UserRefreshToken>> GetUserRefreshTokensByUserIdAsync(string userId)
    {
        return await context.UserRefreshTokens.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<int> AddUserRefreshTokenAsync(UserRefreshToken tokenObj)
    {
        await context.UserRefreshTokens.AddAsync(tokenObj);
        await context.SaveChangesAsync();
        return tokenObj.Id;
    }

    public async Task DeleteUserRefreshToken(UserRefreshToken tokenObj)
    {
        context.UserRefreshTokens.Remove(tokenObj);
        await context.SaveChangesAsync();
    }

    public Task<bool> IsRefreshTokenValidAsync(string token)
    {
        return context.UserRefreshTokens.AnyAsync(t => t.Token == token && DateTime.Now < t.ExpiresAt);
    }

    public async Task RevokeUserRefreshToken(int id)
    {
        var tokenObj = context.UserRefreshTokens.FindAsync(id).Result;
        tokenObj.IsRevoked = true;
        context.UserRefreshTokens.Update(tokenObj);
        await context.SaveChangesAsync();
    }
}
