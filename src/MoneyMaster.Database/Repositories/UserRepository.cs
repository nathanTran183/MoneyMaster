using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task SaveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
