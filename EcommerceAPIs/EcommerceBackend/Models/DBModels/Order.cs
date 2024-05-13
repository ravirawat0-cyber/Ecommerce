namespace EcommerceBackend.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string TransactionId { get; set; } = "";
        public string ReceiptURL { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
