using Microsoft.AspNetCore.SignalR;

namespace EcommerceBackend.Models.Request
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        //user id will be extracted from token claim from userData
    }
}
