using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IAssetAccountRepository
    {
        Task<AssetAccount?> GetAssetAccountByIdAsync(int id);
        Task<IEnumerable<AssetAccount>> GetAssetAccountsAsync();
        Task<int> CreateAssetAccountAsync(AssetAccount assetAccount);
        Task<bool> UpdateAssetAccountAsync(AssetAccount account);
        Task<bool> DeleteAssetAccountAsync(int id);
    }
}
