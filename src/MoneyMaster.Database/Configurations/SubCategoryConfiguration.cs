using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable(nameof(SubCategory));
            builder.HasKey(x => x.Id);

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

            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            builder.Property(w => w.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(f => f.CreatedAt)
                .IsRequired();
            builder.Property(f => f.UpdatedAt)
                .IsRequired();
        }
    }
}
