using MoneyMaster.Common.Enums;

namespace MoneyMaster.Common.Models.Requests
{
    public class UpsertAssetAccountRequest
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public string UserId { get; set; }
    }
}
