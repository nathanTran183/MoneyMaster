using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.ToTable(nameof(FamilyMember));
            builder.HasKey(fm => new
            {
                fm.FamilyId,
                fm.MemberId
            });

            builder.HasOne(fm => fm.Family)
                .WithMany(fm => fm.FamilyMembers)
                .HasForeignKey(fm => fm.FamilyId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(fm => fm.Member)
                .WithMany(fm => fm.FamilyMembers)
                .HasForeignKey(fm => fm.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(w => w.Role)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(f => f.JoinAt)
                .IsRequired();
            builder.Property(f => f.Status)
                .IsRequired();
        }
    }
}
