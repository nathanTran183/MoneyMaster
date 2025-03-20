using MoneyMaster.Common.Enums;

namespace MoneyMaster.Database.Entities
{
    public class FamilyMember
    {
        public int MemberId { get; set; }
        public int FamilyId { get; set; }
        public FamilyMemberRole Role { get; set; }
        public DateTime JoinAt { get; set; }
        public int Status { get; set; }

        public virtual required User Member { get; set; }
        public virtual required Family Family { get; set; }
    }
}
