namespace MoneyMaster.Database.Entities
{
    public class FamilyMember
    {
        public int FamilyId { get; set; }
        public required Family Family { get; set; }
        public int MemberId { get; set; }
        public required User Member { get; set; }
        public string Role { get; set; }
        public DateTime JoinAt { get; set; }
        public int Status { get; set; }
    }
}
