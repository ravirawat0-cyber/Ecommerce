namespace EcommerceBackend.Models.Response
{

    public class Item
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
    }

    public class CartResponse
    {
        public List<Item> Items { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; } 

    }
}
