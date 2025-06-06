using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly MoneyMasterContext context;

        public CategoryRepository(MoneyMasterContext context) 
        { 
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId)
        {
            return await context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category.Id;
        }

        public Task<bool> CategoryNameExistByUserId(int id, string userId, string name)
        {
            return context.Categories.AnyAsync(c => c.Id != id && c.UserId == userId && c.Name == name);
        }

        public Task DeleteCategoryAsync(Category category)
        {
            context.Categories.Remove(category);
            return context.SaveChangesAsync();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            return context.SaveChangesAsync();
        }
    }
}
