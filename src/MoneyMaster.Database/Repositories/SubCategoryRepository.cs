using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public Task<int> AddSubCategoryAsync(SubCategory SubCategory)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSubCategoryAsync(SubCategory account)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubCategory>> GetSubCategoriesByCreatorIdAsync(string creatorId)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory?> GetSubCategoryByCategoryIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubCategory>> GetSubCategorysAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubCategoryNameExistByCategoryId(int id, string categoryId, string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSubCategoryAsync(SubCategory account)
        {
            throw new NotImplementedException();
        }
    }
}
