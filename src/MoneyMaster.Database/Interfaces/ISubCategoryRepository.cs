using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Interfaces;

public interface ISubCategoryRepository
{
    Task<IEnumerable<SubCategory>> GetSubCategoriesAsync();
    Task<SubCategory?> GetSubCategoryByIdAsync(int id);
    Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    Task<int> AddSubCategoryAsync(SubCategory subCategory);
    Task UpdateSubCategoryAsync(SubCategory subCategory);
    Task DeleteSubCategoryAsync(SubCategory subCategory);
    Task<bool> SubCategoryNameExistByCategoryId(int id, int categoryId, string name);
}
