using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;

        public CartServices(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public CartResponse GetItemsByUserId(int userId)
        {
            var userItemsFromDb =  _cartRepository.GetItemsByUserId(userId);

            var items = userItemsFromDb.Select(u => new Item
            {
                ProductId = u.ProductId,
                ProductName = u.ProductName,
                ProductPrice = u.ProductPrice,
                ProductImage = u.ProductImage,
                Quantity = u.Quantity,
            }).ToList();

            var totalItems = items.Sum(i => i.Quantity);

            var cart = new CartResponse
            {
                Items = items,
                TotalItems = totalItems,
                statusMessage = "Success"
            };
            return cart;
        }

        public int AddItemToCart(CartRequest cart, string userId)
        {
            var cartId = _cartRepository.AddItemsToCart(cart, Convert.ToInt32(userId));
            return cartId;

        }

        public void UpdateCart(CartRequest cart, string userId)
        {
            _cartRepository.UpdateCart(cart , Convert.ToInt32(userId));
        }
    }
}
