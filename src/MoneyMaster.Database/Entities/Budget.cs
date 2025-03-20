namespace MoneyMaster.Database.Entities
{
    public class Budget : BaseEntity
    {
        public int SubCategoryId { get; set; }
        public float Amount { get; set; }
        public DateTime Month { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}
