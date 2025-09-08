namespace MoneyMaster.Common.Models.Requests.Category;

public class UpsertSubCategoryRequest 
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public int CategoryId { get; set; }
    public string UserId { get; set; }
}
