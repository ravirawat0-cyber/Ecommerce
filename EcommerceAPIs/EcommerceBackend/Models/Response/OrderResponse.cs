using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Models.Response
{
    public class OrderResponse
    {
        public IEnumerable<Order> Data { get; set; }

        public string StatusMessage { get; set; }
    }
}
