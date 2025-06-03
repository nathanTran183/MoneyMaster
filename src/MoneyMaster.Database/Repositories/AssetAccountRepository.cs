using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories;

public class AssetAccountRepository : IAssetAccountRepository
{
    private readonly MoneyMasterContext context;
    public AssetAccountRepository(MoneyMasterContext context)
    {
        this.context = context;
    }

    public async Task<AssetAccount?> GetAssetAccountByIdAsync(int id)
    {
        return await context.AssetAccounts.FindAsync(id);
    }

    public async Task<IEnumerable<AssetAccount>> GetAssetAccountsAsync()
    {
        return await context.AssetAccounts.ToListAsync();
    }

    public async Task<IEnumerable<AssetAccount>> GetAssetAccountsByUserIdAsync(string userId)
    {
        return await context.AssetAccounts.Where(aa => aa.UserId == userId).ToListAsync();
    }

    public async Task<int> AddAssetAccountAsync(AssetAccount assetAccount)
    {
        await context.AssetAccounts.AddAsync(assetAccount);
        await context.SaveChangesAsync();
        return assetAccount.Id;
    }

    public Task UpdateAssetAccountAsync(AssetAccount assetAccount)
    {
        context.AssetAccounts.Update(assetAccount);
        return context.SaveChangesAsync();
    }

    public Task DeleteAssetAccountAsync(AssetAccount assetAccount)
    {
        context.AssetAccounts.Remove(assetAccount);
        return context.SaveChangesAsync();
    }

    public Task<bool> AssetAccountNameExistByUserId(int id, string userId, string name)
    {
        return context.AssetAccounts.AnyAsync(a => a.Id != id && a.Name == name && a.UserId == userId);
    }
}
