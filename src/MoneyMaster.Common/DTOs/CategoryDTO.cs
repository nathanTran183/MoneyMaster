using MoneyMaster.Database.Entities;
using System.Collections.Generic;

namespace MoneyMaster.Common.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
