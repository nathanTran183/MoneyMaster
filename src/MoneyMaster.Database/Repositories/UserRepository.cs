using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;

namespace MoneyMaster.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MoneyMasterContext context;
        public UserRepository(MoneyMasterContext context) 
        {
            this.context = context;
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> SaveUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user.Id; 
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
