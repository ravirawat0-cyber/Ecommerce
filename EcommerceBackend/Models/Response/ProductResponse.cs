using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class SubCategoryInfo
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
    }
    public class CategoryInfo
    {
        public string CategoryName { get; set; }
        public List<SubCategoryInfo> SubCategories { get; set; }
    }

    public class ProductInfo
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<CategoryInfo> Categories { get; set; }
    }

    public class ProductResponse
    {
        public List<ProductInfo> Products { get; set; }
    }
}
