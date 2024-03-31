namespace EcommerceBackend.Models.DBModels
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
     
    }
}
