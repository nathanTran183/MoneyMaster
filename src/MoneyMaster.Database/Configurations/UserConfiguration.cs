using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);

            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            builder.Property(w => w.Username)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(w => w.PasswordHash)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(w => w.FullName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(f => f.CreatedAt)
                .IsRequired();
            builder.Property(f => f.UpdatedAt)
                .IsRequired();
        }
    }
}
