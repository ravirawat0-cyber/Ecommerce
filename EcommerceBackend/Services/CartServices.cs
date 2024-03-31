using EcommerceBackend.Models.DBModels;
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

        public IEnumerable<UserItems> GetItemsByUserId(int userId)
        {
            return _cartRepository.GetItemsByUserId(userId);
        }
    }
}
