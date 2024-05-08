using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services
{
    public class WishlistServices : IWishlistServices
    {
        private readonly IWishlistRepository _wishlistRepository;


        public WishlistServices(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public WishlistResponse GetWishlistItemsByUserId(int userId)
        {
            var userItemsFromDb = _wishlistRepository.GetItemsByUserId(userId);

            var items = userItemsFromDb.Select(u => new WishlistItems
            {
                ProductId = u.ProductId,
                ProductName = u.ProductName,
                ProductPrice = u.ProductPrice,
                ProductImage = u.ProductImage
            }).ToList();

            var totalItems = items.Count;

            var Wishlist = new WishlistResponse
            {
                Items = items,
                TotalItems = totalItems
            };
            return Wishlist;
        }

        public int AddItemToWishlist(WishlistRequest request, string userId)
        {
            var userID = Convert.ToInt32(userId);
            if (_wishlistRepository.CheckProductWithUserExist( request.ProductId, userID))
                throw new KeyNotFoundException("Product already in wishlist.");
            var cartId = _wishlistRepository.AddItemsToWishlist(request, userID);
            return cartId;
        }


        public void DeleteCartItem(int productId, string userId)
        {
            _wishlistRepository.DeleteWishlistItem(productId, Convert.ToInt32(userId));
        }
    }
}
