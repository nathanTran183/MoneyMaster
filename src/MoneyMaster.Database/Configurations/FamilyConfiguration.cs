using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class FamilyConfiguration : BaseEntityConfiguration<Family>
    {
        public override void Configure(EntityTypeBuilder<Family> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Family));

            builder.HasOne(f => f.User)
                .WithMany(c => c.Families)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasIndex(f => new { f.Name, f.UserId }).IsUnique();
            builder.Property(f => f.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
