using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<UserItems> GetItemsByUserId(int userId);
    }
}
