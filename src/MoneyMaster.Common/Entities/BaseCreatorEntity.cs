namespace MoneyMaster.Database.Entities
{
    public class BaseCreatorEntity : BaseEntity
    {
        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }
    }
}
