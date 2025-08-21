using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget?> GetBudgetByIdAsync(int id);
        Task<IEnumerable<Budget>> GetBudgetsAsync();
        Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(string userId);
        Task<int> AddBudgetAsync(Budget budget);
        Task UpdateBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(Budget budget);
        Task<bool> IsBudgetExisted(string userId, DateTime date, int subCategoryId);
    }
}
