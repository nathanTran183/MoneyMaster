namespace MoneyMaster.Database.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public required int CategoryId { get; set; }
        public int CreatorId { get; set; }

        public User Creator { get; set; }
        public Category Category { get; set; }
        public ICollection<Budget> Budgets { get; set; } = [];
    }
}
