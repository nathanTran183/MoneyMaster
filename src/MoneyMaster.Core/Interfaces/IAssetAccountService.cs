using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAssetAccountService
    {
        Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsAsync();
        Task<ServiceResult<AssetAccountDTO>> GetAssetAccountByIdAsync(int id);
        Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorIdAsync(string creatorId);
        Task<ServiceResult<int>> AddAssetAccountAsync(AssetAccountDTO assetAccountDTO);
        Task<ServiceResult> UpdateAssetAccountAsync(AssetAccountDTO assetAccountDTO);
        Task<ServiceResult> DeleteAssetAccountAsync(int id);
    }
}
