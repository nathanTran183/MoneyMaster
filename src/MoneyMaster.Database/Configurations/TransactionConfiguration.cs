﻿using Microsoft.EntityFrameworkCore;
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

            builder.HasOne(aa => aa.User)
                .WithMany(aa => aa.Transactions)
                .HasForeignKey(aa => aa.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(aa => aa.Family)
                .WithMany(aa => aa.Transactions)
                .HasForeignKey(aa => aa.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(aa => aa.SubCategory)
                .WithMany(aa => aa.Transactions)
                .HasForeignKey(aa => aa.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(aa => aa.AssetAccount)
                .WithMany(aa => aa.Transactions)
                .HasForeignKey(aa => aa.AssetAccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(aa => aa.Amount)
                .IsRequired();
            builder.Property(aa => aa.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(aa => aa.Note);
            builder.Property(aa => aa.TransactionDate)
                .IsRequired();
        }
    }
}
