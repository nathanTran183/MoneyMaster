using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(string id);
        public Task<User> SaveUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);
        public Task<bool> IsEmailExistAsync(string email);
    }
}
