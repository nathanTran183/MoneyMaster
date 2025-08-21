using MoneyMaster.Common.Enums;
using System;

namespace MoneyMaster.Common.Entities
{
    public class Transaction : BaseEntity
    {
        public float Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Note { get; set; }
        public DateTime TransactionDate { get; set; }
        public int SubCategoryId { get; set; }
        public int? FamilyId { get; set; }
        public int AssetAccountId { get; set; }
        public int? TransferTransactionId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual Family? Family { get; set; }
        public virtual AssetAccount AssetAccount { get; set; }
        public virtual Transaction TransferTransaction { get; set; }
    }
}
