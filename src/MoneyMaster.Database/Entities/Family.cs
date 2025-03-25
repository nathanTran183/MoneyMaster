namespace MoneyMaster.Database.Entities
{
    public class Family : BaseCreatorEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = [];
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
        public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = [];
    }
}
