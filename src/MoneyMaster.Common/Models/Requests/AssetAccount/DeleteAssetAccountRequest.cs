using MoneyMaster.Common.Interfaces;

namespace MoneyMaster.Common.Models.Requests.AssetAccount
{
    public class DeleteAssetAccountRequest : IRequest
    {
        public string RequestorId { get; set; }
        public int Id { get; set; }
    }
}
