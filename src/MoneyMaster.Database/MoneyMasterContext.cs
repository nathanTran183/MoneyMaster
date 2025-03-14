using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database.Configurations;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database
{
    public class MoneyMasterContext : DbContext
    {
        public MoneyMasterContext(DbContextOptions<MoneyMasterContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
