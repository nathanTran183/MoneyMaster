using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable(nameof(Budget));
            builder.HasKey(x => x.Id);
            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.Budgets)
                .HasForeignKey(fm => fm.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(fm => fm.SubCategory)
                .WithMany(fm => fm.Budgets)
                .HasForeignKey(fm => fm.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            builder.Property(w => w.Amount)
                .IsRequired();
            builder.Property(w => w.Month)
                .IsRequired();
            builder.Property(f => f.CreatedAt)
                .IsRequired();
            builder.Property(f => f.UpdatedAt)
                .IsRequired();
        }
    }
}
