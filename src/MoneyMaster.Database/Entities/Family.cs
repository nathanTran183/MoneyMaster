﻿namespace MoneyMaster.Database.Entities
{
    public class Family : BaseEntity
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }

        public User Creator { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; } = [];
    }
}
