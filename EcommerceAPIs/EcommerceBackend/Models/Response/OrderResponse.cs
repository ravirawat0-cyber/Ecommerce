using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class OrderResponseModel
    {
        public class OrderResponse
        {
            public string StatusMessage { get; set; }
            public OrderData Data { get; set; }
        }

        public class OrderData
        {
            public List<OrderDetails> OrderDetails { get; set; }
        }

        public class OrderDetails
        {
            public decimal TotalPrice { get; set; }
            public IEnumerable<OrderItem> ItemDetails { get; set; }
            public DateTime OrderDate { get; set; }
            public string TransactionId { get; set; }

            public string ReceiptURL { get; set; }
        }


        public class TransactionOrderResponse
        {
            public Order Data { get; set; }
            public string StatusMessage { get; set; }
        }

    }
}





