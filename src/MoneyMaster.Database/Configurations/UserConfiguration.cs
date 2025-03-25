using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(User));

            builder.HasIndex(w => w.Username)
                .IsUnique();
            builder.Property(w => w.Username)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(w => w.PasswordHash)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(w => w.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(w => w.FullName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(w => w.Avatar)
                .HasMaxLength(250);
        }
    }
}
