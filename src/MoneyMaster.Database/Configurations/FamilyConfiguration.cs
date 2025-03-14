﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    internal class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable(nameof(Family));
            builder.HasKey(x => x.Id);
            builder.HasOne(fm => fm.Creator)
                .WithMany(fm => fm.Families)
                .HasForeignKey(fm => fm.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            builder.Property(w => w.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(f => f.CreatedAt)
                .IsRequired();
            builder.Property(f => f.UpdatedAt)
                .IsRequired();
        }
    }
}
