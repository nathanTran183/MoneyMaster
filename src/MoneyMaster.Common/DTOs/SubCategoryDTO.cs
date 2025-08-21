namespace MoneyMaster.Common.DTOs
{
    public class SubCategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
