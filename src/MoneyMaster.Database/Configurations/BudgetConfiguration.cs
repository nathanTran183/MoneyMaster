using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations;

public class BudgetConfiguration : BaseEntityConfiguration<Budget>
{
    public override void Configure(EntityTypeBuilder<Budget> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(Budget));
        
        builder.HasOne(b => b.User)
            .WithMany(b => b.Budgets)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        builder.HasOne(b => b.SubCategory)
            .WithMany(b => b.Budgets)
            .HasForeignKey(b => b.SubCategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(b => b.Amount)
            .IsRequired();
        builder.Property(b => b.Month)
            .IsRequired();
    }
}
