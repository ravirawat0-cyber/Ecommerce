using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IWishlistServices
    {
        WishlistResponse GetWishlistItemsByUserId(int userId);
        int AddItemToWishlist(WishlistRequest request, string userId);
        void DeleteCartItem(int productId, string userId);
    }
}
