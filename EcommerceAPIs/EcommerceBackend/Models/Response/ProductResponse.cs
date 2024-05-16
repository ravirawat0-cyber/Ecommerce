using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class CategoryInfo
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class SubCategoryInfo
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set;}
    }
    public class Categories
    {
        public CategoryInfo Parentcategory { get; set; }
        public SubCategoryInfo Subcategories { get; set; }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Categories Categories { get; set; }
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

    // Product Response model based on subcategory id


    public class ProductDetailsResponse
    {
        public IEnumerable<ProductDetails> Products { get; set; }
        public string StatusMessage { get; set; }
    }


    public class ProductProfileResponse
    {
        public Products Products { get; set; }

        public string StatusMessage  {get ; set; }
    }
   
}
