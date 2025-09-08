using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories;

public class SubCategoryRepository : ISubCategoryRepository
{
    readonly MoneyMasterContext context;

    public SubCategoryRepository(MoneyMasterContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync()
    {
        return await context.SubCategories.ToListAsync();
    }

    public async Task<SubCategory?> GetSubCategoryByIdAsync(int id)
    {
        return await context.SubCategories.FindAsync(id);
    }

    public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId)
    {
        return await context.SubCategories.Where(sc => sc.CategoryId == categoryId).ToListAsync();
    }

    public async Task<int> AddSubCategoryAsync(SubCategory subCategory)
    {
        await context.SubCategories.AddAsync(subCategory);
        await context.SaveChangesAsync();
        return subCategory.Id;
    }

    public Task UpdateSubCategoryAsync(SubCategory subCategory)
    {
        context.SubCategories.Update(subCategory);
        return context.SaveChangesAsync();
    }

    public Task DeleteSubCategoryAsync(SubCategory subCategory)
    {
        context.SubCategories.Remove(subCategory);
        return context.SaveChangesAsync();
    }

    public Task<bool> SubCategoryNameExistByCategoryId(int id, int categoryId, string name)
    {
        return context.SubCategories.AnyAsync(sc => sc.Id != id && sc.CategoryId == categoryId && sc.Name == name);
    }
}
