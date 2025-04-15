using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class DebtLoanConfiguration : BaseEntityConfiguration<DebtLoan>
    {
        public override void Configure(EntityTypeBuilder<DebtLoan> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(DebtLoan));

            builder.HasOne(dl => dl.Creator)
                .WithMany(c => c.DebtLoans)
                .HasForeignKey(dl => dl.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(dl => dl.AssetAccount)
                .WithMany(aa => aa.DebtLoans)
                .HasForeignKey(dl => dl.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(dl => dl.Amount)
                .IsRequired();
            builder.Property(dl => dl.StartDate)
                .IsRequired();
            builder.Property(dl => dl.EndDate)
                .IsRequired();
        }
    }
}
