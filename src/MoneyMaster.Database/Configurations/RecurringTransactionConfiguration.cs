using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class RecurringTransactionConfiguration : BaseEntityConfiguration<RecurringTransaction>
    {
        public override void Configure(EntityTypeBuilder<RecurringTransaction> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(RecurringTransaction));

            builder.HasOne(aa => aa.Creator)
                .WithMany(aa => aa.RecurringTransactions)
                .HasForeignKey(aa => aa.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(aa => aa.Family)
                .WithMany(aa => aa.RecurringTransactions)
                .HasForeignKey(aa => aa.FamilyId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(aa => aa.SubCategory)
                .WithMany(aa => aa.RecurringTransactions)
                .HasForeignKey(aa => aa.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(aa => aa.AssetAccount)
                .WithMany(aa => aa.RecurringTransactions)
                .HasForeignKey(aa => aa.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(aa => aa.Amount)
                .IsRequired();
            builder.Property(aa => aa.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(aa => aa.Note)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(aa => aa.StartDate)
                .IsRequired();
            builder.Property(aa => aa.Frequency)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
