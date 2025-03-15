using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class SubCategoryConfiguration : BaseEntityConfiguration<SubCategory>
    {
        public override void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(SubCategory));

            builder.HasOne(fm => fm.Category)
                .WithMany(fm => fm.SubCategories)
                .HasForeignKey(fm => fm.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.SubCategories)
                .HasForeignKey(fm => fm.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(w => w.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
