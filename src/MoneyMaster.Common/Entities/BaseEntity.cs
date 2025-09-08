using System;

namespace MoneyMaster.Common.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserId { get; set; }

    public User User { get; set; }
}
