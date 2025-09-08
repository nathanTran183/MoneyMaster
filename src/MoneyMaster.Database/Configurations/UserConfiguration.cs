using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.Property(w => w.FirstName)
            .HasMaxLength(150);
        builder.Property(w => w.LastName)
            .HasMaxLength(150);
        builder.Property(w => w.Avatar)
            .HasMaxLength(250);
    }
}
