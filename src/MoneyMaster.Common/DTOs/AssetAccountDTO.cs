using MoneyMaster.Common.Enums;

namespace MoneyMaster.Common.DTOs
{
    public class AssetAccountDTO : BaseDTO
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
    }
}
