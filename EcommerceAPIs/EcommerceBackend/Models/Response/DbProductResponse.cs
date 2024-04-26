namespace EcommerceBackend.Models.Response
{
    public class DbProductResponse
    {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string CompanyName { get; set; }
            public int Sold { get; set; }
            public string CoverImage { get; set; }
            public string KeyFeature { get; set; } 
            public string ImageUrls { get; set; }
            public string CategoryName { get; set; }
            public string CategoryId { get; set; }
            public int SubcategoryId { get; set; }
            public string SubcategoryName { get; set; }
        
    }
}
