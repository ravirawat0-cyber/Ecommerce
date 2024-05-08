namespace EcommerceBackend.Models.Response
{
    public class WishlistItems
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
    public class WishlistResponse
    {
        public IEnumerable<WishlistItems> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
