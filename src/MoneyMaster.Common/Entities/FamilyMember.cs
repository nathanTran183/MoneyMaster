using MoneyMaster.Common.Enums;
using System;

namespace MoneyMaster.Database.Entities
{
    public class FamilyMember
    {
        public string MemberId { get; set; }
        public int FamilyId { get; set; }
        public FamilyMemberRole Role { get; set; }
        public DateTime JoinAt { get; set; }
        public int Status { get; set; }

        public virtual User Member { get; set; }
        public virtual Family Family { get; set; }
    }
}
