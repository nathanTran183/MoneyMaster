using AutoMapper;
using Microsoft.Extensions.Logging;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class BudgetService : IBudgetService
    {
        ILogger logger;
        IMapper mapper;
        IBudgetRepository budgetRepository;

        public BudgetService(ILogger logger, IMapper mapper, IBudgetRepository budgetRepository) 
        {
            this.logger = logger;
            this.mapper = mapper;
            this.budgetRepository = budgetRepository;
        }

        public async Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsAsync()
        {
            var budgets = await budgetRepository.GetBudgetsAsync();
            var result = new ServiceResult<IEnumerable<BudgetDTO>>();
            result.Value = mapper.Map<IEnumerable<BudgetDTO>>(budgets);
            return result;
        }

        public Task<ServiceResult<BudgetDTO>> GetBudgetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<BudgetDTO>>> GetBudgetsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> AddBudgetAsync(BudgetDTO BudgetDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteBudgetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateBudgetAsync(BudgetDTO BudgetDTO)
        {
            throw new NotImplementedException();
        }
    }
}
