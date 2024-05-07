
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ICartServices
    {
        CartResponse GetItemsByUserId(int userId);
        int AddItemToCart(CartRequest cart, string userId);
        void UpdateCart(CartRequest cart, string userId);

        void DeleteCartItem(int productId, string userId);
        void DeleteCart(string userId);
    }
}
