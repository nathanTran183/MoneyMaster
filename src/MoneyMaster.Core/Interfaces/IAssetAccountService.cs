using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAssetAccountService
    {
        public Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsAsync();
        public Task<ServiceResult<AssetAccountDTO>> GetAssetAccountByIdAsync(int id);
        public Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorIdAsync(string creatorId);
        public Task<ServiceResult<int>> AddAssetAccountAsync(AssetAccountDTO assetAccountDTO);
        public Task<ServiceResult> UpdateAssetAccountAsync(AssetAccountDTO assetAccountDTO);
        public Task<ServiceResult> DeleteAssetAccountAsync(int id);
    }
}
