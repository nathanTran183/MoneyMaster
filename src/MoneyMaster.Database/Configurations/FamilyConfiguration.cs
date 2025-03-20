using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class FamilyConfiguration : BaseEntityConfiguration<Family>
    {
        public override void Configure(EntityTypeBuilder<Family> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Family));

            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.Families)
                .HasForeignKey(fm => fm.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasIndex(w => w.Name).IsUnique();
            builder.Property(w => w.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
