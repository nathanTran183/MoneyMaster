using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IBudgetService
    {
        Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsAsync();
        Task<ServiceResult<BudgetDTO>> GetBudgetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsByUserIdAsync(string userId);
        Task<ServiceResult<int>> AddBudgetAsync(BudgetDTO budgetDTO);
        Task<ServiceResult> UpdateBudgetAsync(BudgetDTO budgetDTO);
        Task<ServiceResult> DeleteBudgetAsync(int id);
    }
}
