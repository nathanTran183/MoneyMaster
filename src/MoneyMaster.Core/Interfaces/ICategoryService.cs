using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces;

public interface ICategoryService
{
    Task<ServiceResult<IEnumerable<CategoryDTO>>> GetCategoriesAsync();
    Task<ServiceResult<CategoryDTO>> GetCategoryByIdAsync(int id);
    Task<ServiceResult<IEnumerable<CategoryDTO>>> GetCategoriesByUserIdAsync(string userId);
    Task<ServiceResult<int>> AddCategoryAsync(CategoryDTO categoryDTO);
    Task<ServiceResult> UpdateCategoryAsync(CategoryDTO categoryDTO);
    Task<ServiceResult> DeleteCategoryAsync(int id);
}
