namespace MoneyMaster.Database.Models
{
    public class FamilyMember
    {
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
        public DateTime JoinAt { get; set; }
        public int Status { get; set; }
    }
}
