using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Entities;
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

        public async Task<ServiceResult<IEnumerable<CategoryDTO>>> GetCategoriesByUserIdAsync(string userId)
        {
            var categories = await categoryRepository.GetCategoriesByUserIdAsync(userId);
            var result = new ServiceResult<IEnumerable<CategoryDTO>>();
            result.Value = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return result;
        }

        public async Task<ServiceResult<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);
            var result = new ServiceResult<CategoryDTO>();
            if (category == null)
            {
                result.AddErrors($"Cannot find Category with Id = {id}");
            }
            else
            {
                result.Value = mapper.Map<CategoryDTO>(category);
            }
            return result;
        }

        public async Task<ServiceResult<int>> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var result = new ServiceResult<int>();
            var isNameExisted = await categoryRepository.CategoryNameExistByUserId(categoryDTO.Id, categoryDTO.UserId, categoryDTO.Name);
            if (isNameExisted)
            {
                result.AddErrors($"The category named {categoryDTO.Name} already existed");
            }
            else
            {
                var category = mapper.Map<Category>(categoryDTO);
                result.Value = await categoryRepository.AddCategoryAsync(category);
            }
            return result;
        }

        public async Task<ServiceResult> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var result = new ServiceResult();
            var category = await categoryRepository.GetCategoryByIdAsync(categoryDTO.Id);
            if (category == null)
            {
                result.AddErrors($"Cannot find Category with Id = {categoryDTO.Id}");
                return result;
            }
            var isDuplicateCategory = await categoryRepository.CategoryNameExistByUserId(categoryDTO.Id, categoryDTO.UserId, categoryDTO.Name);
            if (isDuplicateCategory)
            {
                result.AddErrors($"The category named {categoryDTO.Name} already existed");
            }
            
            await categoryRepository.UpdateCategoryAsync(mapper.Map<Category>(categoryDTO));
            return result;
        }

        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResult();
            var category = await categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                result.AddErrors($"Cannot find Category with Id = {id}");
            }
            else
            {
                await categoryRepository.DeleteCategoryAsync(category);
            }
            return result;
        }
    }
}
