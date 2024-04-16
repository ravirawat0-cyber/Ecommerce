
using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ICartServices
    {
        IEnumerable<UserItems> GetItemsByUserId(int userId);
    }
}
