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
        await _context.SaveChangesAsync();
        return assetAccount.Id;
    }

    public Task UpdateAssetAccountAsync(AssetAccount assetAccount)
    {
        _context.AssetAccounts.Update(assetAccount);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAssetAccountAsync(AssetAccount assetAccount)
    {
        _context.AssetAccounts.Remove(assetAccount);
        return _context.SaveChangesAsync();
    }

    public async Task<bool> AssetAccountNameExistByCreatorId(int id, string creatorId, string name)
    {
        return await _context.AssetAccounts.AnyAsync(a => a.Id != id && a.Name == name && a.CreatorId == creatorId);
    }
}
