using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Category));
            
            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.Categories)
                .HasForeignKey(fm => fm.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(w => w.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
