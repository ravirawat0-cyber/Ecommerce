using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IOrderServices
    {
        int Create(Order request);
        OrderResponse GetAllByUserId(string email);
        OrderResponse GetByTransactionId(string transactionId);
    }
}
