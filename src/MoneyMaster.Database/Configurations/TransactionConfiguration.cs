using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Transaction));

            builder.HasOne(t => t.User)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(t => t.Family)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.SubCategory)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(t => t.AssetAccount)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(t => t.TransferTransaction)
                .WithOne(t => t.TransferTransaction)
                .HasForeignKey<Transaction>(t => t.TransferTransactionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(t => t.Amount)
                .IsRequired();
            builder.Property(t => t.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.Note);
            builder.Property(t => t.TransactionDate)
                .IsRequired();
        }
    }
}
