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

            builder.HasOne(aa => aa.Creator)
                .WithMany(aa => aa.DebtLoans)
                .HasForeignKey(aa => aa.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(aa => aa.AssetAccount)
                .WithMany(aa => aa.DebtLoans)
                .HasForeignKey(aa => aa.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(aa => aa.Amount)
                .IsRequired();
            builder.Property(aa => aa.StartDate)
                .IsRequired();
            builder.Property(aa => aa.EndDate)
                .IsRequired();
        }
    }
}
