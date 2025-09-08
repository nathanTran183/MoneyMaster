using System;

namespace MoneyMaster.Common.Models.Requests;

public class UpsertBudgetRequest
{
    public float Amount { get; set; }
    public DateTime Month {  get; set; }
    public int SubCategoryId { get; set; }
    public string UserId { get; set; }
}
