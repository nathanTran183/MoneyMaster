using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations;

public class UserRefreshTokenConfiguration : BaseEntityConfiguration<UserRefreshToken>
{
    public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        base.Configure(builder);
        builder.ToTable(nameof(UserRefreshToken));
        
        builder.HasOne(t => t.User)
            .WithMany(t => t.UserRefreshTokens)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(t => new { t.Token, t.UserId }).IsUnique();
        builder.Property(t => t.Token)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(t => t.IsActive)
            .HasDefaultValue(true)
            .IsRequired();
        builder.Property(t => t.IsRevoked)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
