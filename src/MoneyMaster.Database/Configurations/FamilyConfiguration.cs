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

            builder.HasOne(f => f.Creator)
                .WithMany(c => c.Families)
                .HasForeignKey(f => f.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasIndex(f => new { f.Name, f.CreatorId }).IsUnique();
            builder.Property(f => f.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
