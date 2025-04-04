using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class AssetAccountConfiguration : BaseEntityConfiguration<AssetAccount>
    {
        public override void Configure(EntityTypeBuilder<AssetAccount> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(AssetAccount));
            
            builder.HasOne(aa => aa.Creator)
                .WithMany(aa => aa.AssetAccounts)
                .HasForeignKey(aa => aa.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasIndex(w => w.Name).IsUnique();
            builder.Property(aa => aa.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(aa => aa.Type)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
