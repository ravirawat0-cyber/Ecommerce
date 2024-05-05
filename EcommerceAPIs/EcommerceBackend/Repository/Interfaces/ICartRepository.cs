using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<Carts> GetItemsByUserId(int userId);
        int AddItemsToCart(CartRequest cart, int userId);
        void UpdateCart(CartRequest cart, int userId);
    }
}
