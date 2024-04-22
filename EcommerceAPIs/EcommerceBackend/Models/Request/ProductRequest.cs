namespace EcommerceBackend.Models.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public int SubCategoryId { get; set; }
        public string ParentCategoryIds { get; set; }
        public bool Sold { get; set; }
        public string KeyFeature { get; set; }
        public string CoverImage { get; set; }
        public string ImageUrls { get; set; }
    }
}
