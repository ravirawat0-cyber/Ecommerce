using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IOrderServices
    {
        int Create(Order request);
        OrderResponse GetAllByUserId(int userId);
        OrderResponse GetByTransactionId(string transactionId);
    }
}
