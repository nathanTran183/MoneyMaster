using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations;

public class SubCategoryConfiguration : BaseEntityConfiguration<SubCategory>
{
    public override void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(SubCategory));

        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasOne(sc => sc.User)
            .WithMany(u => u.SubCategories)
            .HasForeignKey(sc => sc.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasIndex(sc => new { sc.Name, sc.CategoryId }).IsUnique();
        builder.Property(sc => sc.Name)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(sc => sc.Icon);
    }
}
