using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        readonly MoneyMasterContext context;
        public TransactionRepository(MoneyMasterContext context) 
        {
            this.context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await context.Transactions.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAssetAccountIdAsync(int assetAccountId)
        {
            return await context.Transactions.Where(t => t.AssetAccountId == assetAccountId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByFamilyIdAsync(int familyId)
        {
            return await context.Transactions.Where(t => t.FamilyId == familyId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(string userId)
        {
            return await context.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> AddTransactionAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            return transaction.Id;
        }

        public Task UpdateTransactionAsync(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            return context.SaveChangesAsync();
        }

        public Task DeleteTransactionAsync(Transaction transaction)
        {
            context.Transactions.Remove(transaction);
            return context.SaveChangesAsync();
        }

        public Task DeleteTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            context.Transactions.RemoveRange(transactions);
            return context.SaveChangesAsync();
        }
    }
}
