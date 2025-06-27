using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common;
using MoneyMaster.Database.Configurations;
using MoneyMaster.Database.Entities;
using System.Collections.Generic;

namespace MoneyMaster.Database
{
    public class MoneyMasterContext : IdentityDbContext<User>
    {
        public MoneyMasterContext(DbContextOptions<MoneyMasterContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<AssetAccount> AssetAccounts { get; set; }
        public DbSet<DebtLoan> DebtLoans { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            // Configure relationships
            builder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FamilyConfiguration());
            builder.ApplyConfiguration(new FamilyMemberConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SubCategoryConfiguration());
            builder.ApplyConfiguration(new BudgetConfiguration());
            builder.ApplyConfiguration(new AssetAccountConfiguration());
            builder.ApplyConfiguration(new DebtLoanConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new RecurringTransactionConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
