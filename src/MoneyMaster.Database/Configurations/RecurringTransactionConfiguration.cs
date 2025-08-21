using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class RecurringTransactionConfiguration : BaseEntityConfiguration<RecurringTransaction>
    {
        public override void Configure(EntityTypeBuilder<RecurringTransaction> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(RecurringTransaction));

            builder.HasOne(rt => rt.User)
                .WithMany(rt => rt.RecurringTransactions)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(rt => rt.Family)
                .WithMany(rt => rt.RecurringTransactions)
                .HasForeignKey(rt => rt.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(rt => rt.SubCategory)
                .WithMany(rt => rt.RecurringTransactions)
                .HasForeignKey(rt => rt.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(rt => rt.AssetAccount)
                .WithMany(rt => rt.RecurringTransactions)
                .HasForeignKey(rt => rt.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(rt => rt.Amount)
                .IsRequired();
            builder.Property(rt => rt.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(rt => rt.Note);
            builder.Property(rt => rt.StartDate)
                .IsRequired();
            builder.Property(rt => rt.Frequency)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
