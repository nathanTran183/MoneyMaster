using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IAssetAccountRepository
    {
        Task<AssetAccount?> GetAssetAccountByIdAsync(int id);
        Task<IEnumerable<AssetAccount>> GetAssetAccountsAsync();
        Task<IEnumerable<AssetAccount>> GetAssetAccountsByCreatorIdAsync(string creatorId);
        Task<int> AddAssetAccountAsync(AssetAccount assetAccount);
        Task UpdateAssetAccountAsync(AssetAccount account);
        Task DeleteAssetAccountAsync(AssetAccount account);
        Task<bool> AssetAccountNameExistByCreatorId(int id, string creatorId, string name);
    }
}
