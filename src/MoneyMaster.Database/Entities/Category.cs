namespace MoneyMaster.Database.Entities
{
    public class Category : BaseCreatorEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
