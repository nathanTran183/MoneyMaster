using MoneyMaster.Common.Enums;
using System.Collections.Generic;

namespace MoneyMaster.Database.Entities
{
    public class AssetAccount : BaseCreatorEntity
    {
        public string Name { get; set; }
        public AssetType Type { get; set; }

        public virtual IEnumerable<DebtLoan> DebtLoans { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }
        public virtual IEnumerable<RecurringTransaction> RecurringTransactions { get; set; }
    }
}
