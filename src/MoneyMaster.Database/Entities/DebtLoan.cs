namespace MoneyMaster.Database.Entities
{
    public class DebtLoan : BaseEntity
    {
        public float Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AssetAccountId { get; set; }

        public virtual AssetAccount AssetAccount { get; set; }
    }
}
