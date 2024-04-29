using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class SubCategoryResponse
    {
       public IEnumerable<SubCategory> Data { get; set; }
       public string StatusMessage { get; set; }
    }

    public class SubCategoryByParentIdResponse
    {
        public IEnumerable<SubCategoryByParentId> Data { get; set; }
        public string StatusMessage { get; set; }
    }
}
