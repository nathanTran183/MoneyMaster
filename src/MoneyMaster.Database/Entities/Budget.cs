namespace MoneyMaster.Database.Entities
{
    public class Budget : BaseEntity
    {
        public int SubCategoryId { get; set; }
        public int CreatorId { get; set; }
        public float Amount { get; set; }
        public DateTime Month { get; set; }

        public SubCategory SubCategory { get; set; }
        public User Creator { get; set; }
    }
}
