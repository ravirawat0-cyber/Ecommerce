namespace EcommerceBackend.Models.Request
{
    public class SubCategoryRequest
    {
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        
        public string ImageUrl { get; set; }
  
    }
}
