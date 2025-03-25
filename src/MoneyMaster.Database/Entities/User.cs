namespace MoneyMaster.Database.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = [];
        public virtual ICollection<Family> Families { get; set; } = [];
        public virtual ICollection<Category> Categories { get; set; } = [];
        public virtual ICollection<SubCategory> SubCategories { get; set; } = [];
        public virtual ICollection<Budget> Budgets { get; set; } = [];
        public virtual ICollection<AssetAccount> AssetAccounts { get; set; } = [];
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = [];
        public virtual ICollection<DebtLoan> DebtLoans { get; set; } = [];
    }
}
