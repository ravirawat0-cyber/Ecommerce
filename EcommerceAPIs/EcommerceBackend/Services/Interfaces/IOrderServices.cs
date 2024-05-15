using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IOrderServices
    {
        int Create(Order request);
        OrderResponseModel.OrderResponse GetAllByUserId(int userId);
        OrderResponseModel.TransactionOrderResponse GetByTransactionId(string transactionId);
        int CreateOrderItem(OrderItem item);
        int hasPurchasedQuery(int userId, int productId);
    }
}
