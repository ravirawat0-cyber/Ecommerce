
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ICartServices
    {
        CartResponse GetItemsByUserId(int userId);
    }
}
