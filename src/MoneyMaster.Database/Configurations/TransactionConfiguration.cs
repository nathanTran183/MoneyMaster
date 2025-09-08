using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Common.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Transaction));

            builder.HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(t => t.Family)
                .WithMany(f => f.Transactions)
                .HasForeignKey(t => t.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.SubCategory)
                .WithMany(sc => sc.Transactions)
                .HasForeignKey(t => t.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(t => t.AssetAccount)
                .WithMany(aa => aa.Transactions)
                .HasForeignKey(t => t.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(t => t.TransferTransaction)
                .WithOne()
                .HasForeignKey<Transaction>(t => t.TransferTransactionId)
                .OnDelete(DeleteBehavior.NoAction);

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
