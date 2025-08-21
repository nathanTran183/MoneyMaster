using System.Collections.Generic;

namespace MoneyMaster.Common.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
