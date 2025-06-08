using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<ServiceResult<IEnumerable<SubCategoryDTO>>> GetSubCategoriesAsync();
        Task<ServiceResult<IEnumerable<SubCategoryDTO>>> GetSubCategoriesByCategoryIdAsync(int categoryId);
        Task<ServiceResult<int>> AddSubCategoryAsync(SubCategoryDTO subCategoryDTO);
        Task<ServiceResult> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDTO);
        Task<ServiceResult> DeleteSubCategoryAsync(int id);
    }
}
