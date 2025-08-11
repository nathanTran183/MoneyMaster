using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<IEnumerable<Transaction>> GetTransactionsByAssetAccountIdAsync(int assetAccountId);
        Task<IEnumerable<Transaction>> GetTransactionsByFamilyIdAsync(int familyId);
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<int> AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Transaction transaction);
        Task DeleteTransactionsAsync(IEnumerable<Transaction> transactions);
    }
}
