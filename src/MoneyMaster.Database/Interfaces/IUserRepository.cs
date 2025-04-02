using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmail(string email);
        public Task SaveUser(User user);
    }
}
