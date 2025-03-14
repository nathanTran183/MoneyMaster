using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(x => x.Id);
            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.Categories)
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
