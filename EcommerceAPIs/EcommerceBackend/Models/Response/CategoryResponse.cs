using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class SubCategoryInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class CategoryInfoResponse
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public IEnumerable<SubCategoryInfoResponse> Subcategories { get; set; }
    }
    public class CategoryWithSubCategoriesResponse
    {
        public IEnumerable<CategoryInfoResponse> Data { get; set; }
        public string StatusMessage { get; set; }
    }

    public class CategoryResponse
    {
        public IEnumerable<Category> Data { get; set; }
        public string StatusMessage { get; set; }
    }
}
