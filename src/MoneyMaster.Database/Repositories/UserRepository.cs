using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MoneyMasterContext _context;
        public UserRepository(MoneyMasterContext context) 
        {
            _context = context;
        }
        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public Task SaveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
