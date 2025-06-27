using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyMaster.Database.Entities
{
    public class User : IdentityUser
    {
        [StringLength(150)]
        public string? FirstName { get; set; }
        [StringLength(150)]
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = null!;
        public virtual ICollection<Family> Families { get; set; } = null!;
        public virtual ICollection<Category> Categories { get; set; } = null!;
        public virtual ICollection<SubCategory> SubCategories { get; set; } = null!;
        public virtual ICollection<Budget> Budgets { get; set; } = null!;
        public virtual ICollection<AssetAccount> AssetAccounts { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; } = null!;
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = null!;
        public virtual ICollection<DebtLoan> DebtLoans { get; set; } = null!;
    }
}
