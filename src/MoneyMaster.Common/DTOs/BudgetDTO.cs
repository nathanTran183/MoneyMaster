using System;

namespace MoneyMaster.Common.DTOs
{
    public class BudgetDTO : BaseDTO
    {
        float Amount { get; set; }
        DateTime Month { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategoryDTO SubCategory { get; set; }
    }
}
