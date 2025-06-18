using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId);
        Task<int> AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<bool> CategoryNameExistByUserId(int id, string userId, string name);
    }
}
