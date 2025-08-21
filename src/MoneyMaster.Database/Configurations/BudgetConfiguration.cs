using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class BudgetConfiguration : BaseEntityConfiguration<Budget>
    {
        public override void Configure(EntityTypeBuilder<Budget> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Budget));
            
            builder.HasOne(fm => fm.User)
                .WithMany(fm => fm.Budgets)
                .HasForeignKey(fm => fm.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(fm => fm.SubCategory)
                .WithMany(fm => fm.Budgets)
                .HasForeignKey(fm => fm.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(w => w.Amount)
                .IsRequired();
            builder.Property(w => w.Month)
                .IsRequired();
        }
    }
}
