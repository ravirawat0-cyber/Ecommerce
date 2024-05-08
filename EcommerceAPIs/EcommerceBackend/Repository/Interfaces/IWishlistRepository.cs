using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IWishlistRepository
    {
        IEnumerable<Wishlist> GetItemsByUserId(int userId);
        int AddItemsToWishlist(WishlistRequest request, int userId);
        void DeleteWishlistItem(int productId, int userId);
        bool CheckProductWithUserExist(int productId, int userId);
    }
}
