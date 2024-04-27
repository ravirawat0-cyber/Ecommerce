namespace EcommerceBackend.Models.Response;

public class DbCategoryResponse
{
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public int SubCategoryId { get; set; }
    public  string SubCategoryName { get; set; }
    public string SubCategoryImageUrl { get; set; }
}