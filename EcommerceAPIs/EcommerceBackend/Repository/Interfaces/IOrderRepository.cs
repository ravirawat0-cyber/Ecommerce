using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IOrderRepository
    {
        int Create(Order request);
        IEnumerable<Order> GetAllByUserId(int userId);

        Order GetByTransactionId(string transactionId);
    }
}
