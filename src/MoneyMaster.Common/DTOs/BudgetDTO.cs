using System;

namespace MoneyMaster.Common.DTOs;

public class BudgetDTO : BaseDTO
{
    public float Amount { get; set; }
    public DateTime Month { get; set; }
    public int SubCategoryId { get; set; }
    public virtual SubCategoryDTO SubCategory { get; set; }
}
