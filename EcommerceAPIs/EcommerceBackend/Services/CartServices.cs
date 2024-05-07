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
            var totalPrice = items.Sum(i => i.Quantity * i.ProductPrice);

            var cart = new CartResponse
            {
                Items = items,
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };
            return cart;
        }

        public int AddItemToCart(CartRequest cart, string userId)
        {
            var userID = Convert.ToInt32(userId);

            if (_cartRepository.CheckProductWithUserExist(userID, cart.ProductId))
                throw new KeyNotFoundException("Product already in cart.");
            var cartId = _cartRepository.AddItemsToCart(cart, userID);
            return cartId;
        }


        public void DeleteCartItem(int productId, string userId)
        {
            _cartRepository.DeleteCartItem(productId, int.Parse(userId));
        }

        public void UpdateCart(CartRequest cart, string userId)
        {
            _cartRepository.UpdateCart(cart , Convert.ToInt32(userId));
        }

        public void DeleteCart(string userId)
        {
            _cartRepository.DeleteCart(int.Parse(userId));
        }
       
    }
}
