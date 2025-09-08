using System.Collections.Generic;

namespace MoneyMaster.Common.Entities;

public class Family : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = null!;
    public virtual ICollection<Transaction> Transactions { get; set; } = null!;
    public virtual ICollection<RecurringTransaction> RecurringTransactions { get; set; } = null!;
}
