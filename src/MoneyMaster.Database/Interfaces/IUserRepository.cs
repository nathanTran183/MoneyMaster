using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(string email);
        public Task<string> SaveUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);
        public Task<bool> IsEmailExistAsync(string email);
    }
}
