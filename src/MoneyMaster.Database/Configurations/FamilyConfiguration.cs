using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Models;

namespace MoneyMaster.Database.Configurations
{
    internal class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable(nameof(Family));
            builder.HasKey(x => x.Id);

            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            builder.Property(w => w.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(f => f.CreatedAt)
                .IsRequired();
            builder.Property(f => f.UpdatedAt)
                .IsRequired();
        }
    }
}
