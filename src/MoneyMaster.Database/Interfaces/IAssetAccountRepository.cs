using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IAssetAccountRepository
    {
        Task<AssetAccount?> GetAssetAccountByIdAsync(int id);
        Task<IEnumerable<AssetAccount>> GetAssetAccountsAsync();
        Task<IEnumerable<AssetAccount>> GetAssetAccountsByUserIdAsync(string userId);
        Task<int> AddAssetAccountAsync(AssetAccount assetAccount);
        Task UpdateAssetAccountAsync(AssetAccount account);
        Task DeleteAssetAccountAsync(AssetAccount account);
        Task<bool> AssetAccountNameExistByUserId(int id, string userId, string name);
    }
}
