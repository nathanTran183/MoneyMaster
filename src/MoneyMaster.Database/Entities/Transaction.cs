﻿using MoneyMaster.Common.Enums;

namespace MoneyMaster.Database.Entities
{
    public class Transaction : BaseCreatorEntity
    {
        public float Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Note { get; set; }
        public DateTime TransactionDate { get; set; }
        public int SubCategoryId { get; set; }
        public int? FamilyId { get; set; }
        public int AssetAccountId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual Family? Family { get; set; }
        public virtual AssetAccount AssetAccount { get; set; }
    }
}
