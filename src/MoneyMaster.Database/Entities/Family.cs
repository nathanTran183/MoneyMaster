namespace MoneyMaster.Database.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = [];
    }
}
