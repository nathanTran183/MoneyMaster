namespace MoneyMaster.Database.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public required int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; } = [];
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = [];
    }
}
