using MoneyMaster.Common.Enums;

namespace MoneyMaster.Common.Models.Requests.AssetAccount
{
    public class AddAssetAccountRequest
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public string UserId { get; set; }
    }
}
