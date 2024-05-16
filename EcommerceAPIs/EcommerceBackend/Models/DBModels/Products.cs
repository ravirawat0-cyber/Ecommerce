namespace EcommerceBackend.Models.DBModels
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public bool Sold { get; set; } = false;
        public string KeyFeature { get; set; }
        public string CoverImage { get; set; }
        public string ImageUrls { get; set; }
        public int AverageRating { get; set; }
        public int TotalRating { get; set; }
    }

    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CoverImage { get; set; }
        public string KeyFeature { get; set; }
        public int AverageRating { get; set; }
        public int TotalRating { get; set; }
    }

}
