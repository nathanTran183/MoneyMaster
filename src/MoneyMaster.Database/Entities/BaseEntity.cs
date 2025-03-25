﻿namespace MoneyMaster.Database.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
