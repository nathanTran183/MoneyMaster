using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories;

public class AssetAccountRepository : IAssetAccountRepository
{
    private readonly MoneyMasterContext _context;
    public AssetAccountRepository(MoneyMasterContext context)
    {
        _context = context;
    }

    public async Task<AssetAccount?> GetAssetAccountByIdAsync(int id)
    {
        return await _context.AssetAccounts.FindAsync(id);
    }

    public async Task<IEnumerable<AssetAccount>> GetAssetAccountsAsync()
    {
        return await _context.AssetAccounts.ToListAsync();
    }

    public async Task<IEnumerable<AssetAccount>> GetAssetAccountsByCreatorIdAsync(string creatorId)
    {
        return await _context.AssetAccounts.Where(aa => aa.CreatorId == creatorId).ToListAsync();
    }

    public async Task<int> AddAssetAccountAsync(AssetAccount assetAccount)
    {
        await _context.AssetAccounts.AddAsync(assetAccount);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAssetAccountAsync(AssetAccount account)
    {
        var assetAccount = await _context.AssetAccounts.FindAsync(account.Id);
        if (assetAccount == null)
        {
            return false;
        }

        assetAccount.Name = account.Name;
        assetAccount.Type = account.Type;
        _context.AssetAccounts.Update(assetAccount);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAssetAccountAsync(int id)
    {
        var assetAccount = await _context.AssetAccounts.FindAsync(id);
        if (assetAccount == null)
        {
            return false;
        }
        _context.AssetAccounts.Remove(assetAccount);
        return await _context.SaveChangesAsync() > 0;
    }
}
