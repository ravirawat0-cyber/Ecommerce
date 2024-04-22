using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class CategoryInfo
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class Categories
    {
        public List<CategoryInfo> CategoryNames { get; set; }
        public string SubCategories { get; set; }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Categories> Categories { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public bool Sold { get; set; }
        public string Keyfeature { get; set; }
        public string CoverImage { get; set; }
        public string ImageUrls { get; set; }
        
    }

    public class ProductResponse
    {
        public List<ProductInfo> Products { get; set; }
    }
}
