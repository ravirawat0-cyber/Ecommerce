using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IOrderRepository
    {
        int Create(Order request);
        IEnumerable<Order> GetAllByUserEmail(string email);

        Order GetByTransactionId(string transactionId);
    }
}
