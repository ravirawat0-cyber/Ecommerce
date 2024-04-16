namespace EcommerceBackend.Models.Response
{
    public class DbProductResponse
    {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string CompanyName { get; set; }
            public int Sold { get; set; }
            public bool IsCustomized { get; set; }
            public string KeyFeature { get; set; }
            public bool IsActive { get; set; }
            public string ImageUrls { get; set; }
            public string CategoryName { get; set; }
            public int SubCategoryID { get; set; }
            public string SubCategoryName { get; set; }
        
    }
}
