namespace EcommerceBackend.Models.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNO { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public bool IsCancel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
