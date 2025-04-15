﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Database.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Category));

            builder.HasOne(c => c.Creator)
                .WithMany(c => c.Categories)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasIndex(c => new { c.Name, c.CreatorId }).IsUnique();
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
