using Microsoft.AspNetCore.SignalR;

namespace EcommerceBackend.Models.DBModels
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        
        public string ImageUrl { get; set; }
     
    }


    public class SubCategoryByParentId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
