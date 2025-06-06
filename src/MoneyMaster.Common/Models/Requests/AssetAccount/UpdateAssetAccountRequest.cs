using MoneyMaster.Common.Enums;

namespace MoneyMaster.Common.Models.Requests.AssetAccount
{
    public class UpdateAssetAccountRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
    }
}
