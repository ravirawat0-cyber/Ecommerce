namespace EcommerceBackend.Models.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public string SubCategoryIds { get; set; }
        public bool Sold { get; set; }
        public bool IsCustomized { get; set; }
        public string KeyFeature { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrls { get; set; }
    }
}
