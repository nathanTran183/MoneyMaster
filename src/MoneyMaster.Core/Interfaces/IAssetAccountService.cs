using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAssetAccountService
    {
        public Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccounts();
        public Task<ServiceResult<AssetAccountDTO>> GetAssetAccountById(int id);
        public Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorId(string creatorId);
        public Task<ServiceResult<int>> CreateAssetAccount(AssetAccountDTO assetAccountDTO, string userId);
        public Task<ServiceResult> UpdateAssetAccount(AssetAccountDTO assetAccountDTO, string userId);
        public Task<ServiceResult> DeleteAssetAccount(int id);
    }
}
