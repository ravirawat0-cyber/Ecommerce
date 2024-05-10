using Microsoft.AspNetCore.SignalR;
using Stripe;

namespace EcommerceBackend.Models.Request
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;

        //user id will be extracted from token claim from userData
    } 
    
    public class CartPurchaseRequest
    {
       public string UUID { get; set; }
    }

   
}
