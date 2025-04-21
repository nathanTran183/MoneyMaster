using MoneyMaster.Common.Interfaces;

namespace MoneyMaster.Common.Models.Requests.AssetAccount
{
    public class GetAssetAccountRequest : IRequest
    {
        public string RequestorId { get;set;}
    }
}
