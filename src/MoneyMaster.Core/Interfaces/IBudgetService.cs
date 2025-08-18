using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface IBudgetService
    {
        Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsAsync();
        Task<ServiceResult<BudgetDTO>> GetBudgetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsByUserIdAsync(string userId);
        Task<ServiceResult<int>> AddBudgetAsync(BudgetDTO BudgetDTO);
        Task<ServiceResult> UpdateBudgetAsync(BudgetDTO BudgetDTO);
        Task<ServiceResult> DeleteBudgetAsync(int id);
    }
}
