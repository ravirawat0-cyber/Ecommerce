namespace EcommerceBackend.Models.DBModels
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        public string ProductImage { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
