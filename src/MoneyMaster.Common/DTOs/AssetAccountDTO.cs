using MoneyMaster.Common.Enums;
using System;

namespace MoneyMaster.Common.DTOs
{
    public class AssetAccountDTO : BaseDTO
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public Guid CreatorId { get; set; }
    }
}
