using MoneyMaster.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMaster.Common.DTOs
{
    public class AssetAccountDTO : BaseDTO
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }
        public Guid CreatorId { get; set; }
    }
}
