using Stripe;

namespace EcommerceBackend.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string TransactionId { get; set; } 
        public int UserId { get; set; }
        public string ReceiptURL { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }


    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
    }
}
