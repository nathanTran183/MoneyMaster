namespace MoneyMaster.Database.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }

        public User Creator { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
