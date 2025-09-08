using System.Collections.Generic;

namespace MoneyMaster.Common.DTOs;

public class CategoryDTO : BaseDTO
{
    public string Name { get; set; }
    public virtual IEnumerable<SubCategoryDTO> SubCategories { get; set; }
}
