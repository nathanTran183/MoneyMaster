using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces;

public interface ITransactionService
{
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactions();
    public Task<ServiceResult<TransactionDTO>> GetTransactionById(int id);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserId(string userId);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByAssetAccountId(int assetAccountId);
    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByFamilyId(int familyId);
    public Task<ServiceResult<int>> AddTransaction(TransactionDTO transactionDTO);
    public Task<ServiceResult> UpdateTransaction(TransactionDTO transactionDTO);
    public Task<ServiceResult> DeleteTransaction(int id);
}
