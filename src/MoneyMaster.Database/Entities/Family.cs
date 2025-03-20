namespace MoneyMaster.Database.Entities
{
    public class Family : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = [];
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = [];
    }
}
