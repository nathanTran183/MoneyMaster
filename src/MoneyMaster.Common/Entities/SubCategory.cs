using System.Collections.Generic;

namespace MoneyMaster.Database.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Icon {  get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; } = null!;
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = null!;
    }
}
