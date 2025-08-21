using MoneyMaster.Common.Enums;
using System;

namespace MoneyMaster.Common.Entities
{
    public class RecurringTransaction : BaseEntity
    {
        public float Amount { get; set; }
        public DateTime StartDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Note { get; set; }
        public Frequency Frequency { get; set; }

        public int SubCategoryId { get; set; }
        public int AssetAccountId { get; set; }
        public int? FamilyId { get; set; }


        public virtual SubCategory SubCategory { get; set; }
        public virtual AssetAccount AssetAccount { get; set; }
        public virtual Family? Family { get; set; }
    }
}
