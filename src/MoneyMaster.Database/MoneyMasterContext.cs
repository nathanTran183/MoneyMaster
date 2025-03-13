using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Configurations;
using MoneyMaster.Database.Models;

namespace MoneyMaster.Database
{
    public class MoneyMasterContext : DbContext
    {
        public MoneyMasterContext(DbContextOptions<MoneyMasterContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
