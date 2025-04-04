using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MoneyMaster.Database.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string? Avatar { get; set; }

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
