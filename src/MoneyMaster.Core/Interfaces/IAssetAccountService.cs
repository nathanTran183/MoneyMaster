using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IAssetAccountService
    {
        public IEnumerable<AssetAccountDTO> GetAssetAccounts();
    }
}
