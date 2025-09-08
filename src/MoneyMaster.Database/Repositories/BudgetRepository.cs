using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories;

public class BudgetRepository : IBudgetRepository
{
    MoneyMasterContext context;
    public BudgetRepository(MoneyMasterContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Budget>> GetBudgetsAsync()
    {
        return await context.Budgets.ToListAsync();
    }

    public async Task<Budget?> GetBudgetByIdAsync(int id)
    {
        return await context.Budgets.FindAsync(id);
    }

    public async Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(string userId)
    {
        return await context.Budgets.Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<int> AddBudgetAsync(Budget budget)
    {
        await context.Budgets.AddAsync(budget);
        await context.SaveChangesAsync();
        return budget.Id;
    }
    public Task UpdateBudgetAsync(Budget budget)
    {
        context.Budgets.Update(budget);
        return context.SaveChangesAsync();
    }

    public Task DeleteBudgetAsync(Budget budget)
    {
        context.Budgets.Remove(budget);
        return context.SaveChangesAsync();
    }

    public Task<bool> IsBudgetExisted(string userId, DateTime date, int subCategoryId)
    {
        return context.Budgets.AnyAsync(b => b.UserId == userId && b.SubCategoryId == subCategoryId && b.Month.Month == date.Month && b.Month.Year == date.Year);
    }
}
