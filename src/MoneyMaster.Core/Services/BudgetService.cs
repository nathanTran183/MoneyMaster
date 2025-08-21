using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class BudgetService : IBudgetService
    {
        IMapper mapper;
        IBudgetRepository budgetRepository;
        IUserRepository userRepository;

        public BudgetService(IMapper mapper, IBudgetRepository budgetRepository, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.budgetRepository = budgetRepository;
            this.userRepository = userRepository;
        }

        public async Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsAsync()
        {
            var budgets = await budgetRepository.GetBudgetsAsync();
            var result = new ServiceResult<IEnumerable<BudgetDTO>>();
            result.Value = mapper.Map<IEnumerable<BudgetDTO>>(budgets);
            return result;
        }

        public async Task<ServiceResult<BudgetDTO>> GetBudgetByIdAsync(int id)
        {
            var result = new ServiceResult<BudgetDTO>();
            var budget = await budgetRepository.GetBudgetByIdAsync(id);
            if (budget == null)
            {
                result.AddErrors($"Budget with Id = {id} is not found.");
            }
            else
            {
                result.Value = mapper.Map<BudgetDTO>(budget);
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsByUserIdAsync(string userId)
        {
            var result = new ServiceResult<IEnumerable<BudgetDTO>>();
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                result.AddErrors($"User with Id = {userId} is not existed.");
                return result;
            }
            var budgets = await budgetRepository.GetBudgetsByUserIdAsync(userId);
            result.Value = mapper.Map<IEnumerable<BudgetDTO>>(budgets);
            return result;
        }

        public async Task<ServiceResult<int>> AddBudgetAsync(BudgetDTO budgetDTO)
        {
            var result = new ServiceResult<int>();
            var isBudgetDuplicate = await budgetRepository.IsBudgetExisted(budgetDTO.UserId, budgetDTO.Month, budgetDTO.SubCategoryId);
            if (isBudgetDuplicate)
            {
                result.AddErrors("The budget is already existed in database.");
                return result;
            }
            result.Value = await budgetRepository.AddBudgetAsync(mapper.Map<Budget>(budgetDTO));
            return result;
        }

        public async Task<ServiceResult> UpdateBudgetAsync(BudgetDTO budgetDTO)
        {
            var result = new ServiceResult();
            var budget = await budgetRepository.GetBudgetByIdAsync(budgetDTO.Id);
            if (budget == null)
            {
                result.AddErrors($"Budget with Id = {budgetDTO.Id} is not found.");
            }
            else
            {
                await budgetRepository.UpdateBudgetAsync(mapper.Map<Budget>(budgetDTO));
            }
            return result;
        }

        public async Task<ServiceResult> DeleteBudgetAsync(int id)
        {
            var result = new ServiceResult();
            var budget = await budgetRepository.GetBudgetByIdAsync(id);
            if (budget == null)
            {
                result.AddErrors($"Budget with Id = {id} is not found.");
            }
            else
            {
                await budgetRepository.DeleteBudgetAsync(budget);
            }
            return result;
        }
    }
}
