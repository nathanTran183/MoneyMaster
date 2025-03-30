using System.Collections.Generic;

namespace MoneyMaster.Database.Entities
{
    public class Family : BaseCreatorEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; } = null!;
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = null!;
    }
}
