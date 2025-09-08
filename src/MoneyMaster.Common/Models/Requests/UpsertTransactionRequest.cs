using MoneyMaster.Common.Enums;
using System;

namespace MoneyMaster.Common.Models.Requests;

public class UpsertTransactionRequest
{
    public float Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? Note { get; set; }
    public DateTime TransactionDate { get; set; }
    public int SubCategoryId { get; set; }
    public int? FamilyId { get; set; }
    public int AssetAccountId { get; set; }
    public int? TransferTransactionId { get; set; }
    public string UserId { get; set; }
}
