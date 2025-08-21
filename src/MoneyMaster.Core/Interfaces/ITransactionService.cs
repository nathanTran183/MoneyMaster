using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces;

public interface ITransactionService
{
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsAsync();
    public Task<ServiceResult<TransactionDTO>> GetTransactionByIdAsync(int id);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserIdAsync(string userId);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByAssetAccountIdAsync(int assetAccountId);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByFamilyIdAsync(int familyId);
    public Task<ServiceResult<int>> AddTransactionAsync(TransactionDTO transactionDTO);
    public Task<ServiceResult> UpdateTransactionAsync(TransactionDTO transactionDTO);
    public Task<ServiceResult> DeleteTransactionAsync(int id);
}
