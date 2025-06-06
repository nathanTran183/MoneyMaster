using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategorysAsync();
        Task<SubCategory?> GetSubCategoryByCategoryIdAsync(int id);
        Task<int> AddSubCategoryAsync(SubCategory SubCategory);
        Task UpdateSubCategoryAsync(SubCategory account);
        Task DeleteSubCategoryAsync(SubCategory account);
        Task<bool> SubCategoryNameExistByCategoryId(int id, string categoryId, string name);
    }
}
