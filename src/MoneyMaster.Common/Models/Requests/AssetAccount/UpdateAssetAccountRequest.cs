using MoneyMaster.Common.Enums;
using MoneyMaster.Common.Interfaces;

namespace MoneyMaster.Common.Models.Requests.AssetAccount
{
    public class UpdateAssetAccountRequest : IRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public string RequestorId { get; set; }
    }
}
