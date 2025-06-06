using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository categoryRepository;
        readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) 
        { 
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            var result = new ServiceResult<IEnumerable<CategoryDTO>>();
            result.Value = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return result;
        }

        public Task<ServiceResult<IEnumerable<CategoryDTO>>> GetCategoriesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
