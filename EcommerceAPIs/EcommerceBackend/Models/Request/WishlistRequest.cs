namespace EcommerceBackend.Models.Request
{
    public class WishlistRequest
    {
        public int ProductId { get; set; }

        // user id will be extracted from token
    }
}
