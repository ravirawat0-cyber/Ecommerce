using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class OrderRepository :BaseRepository<Order>, IOrderRepository
    {
        private readonly DbContext _dbContext;
        public OrderRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public int Create(Order request)
        {
            var query = @"
                         INSERT INTO Orders
                         (UserEmail, TransactionId, ReceiptURL, TotalPrice, OrderDate)
                         VALUES 
                          (@UserEmail, @TransactionId, @ReceiptURL, @TotalPrice, @OrderDate)
                          SELECT SCOPE_IDENTITY()
                         ";
            var values = new
            {
                UserEmail = request.UserEmail,
                TransactionId = request.TransactionId,
                ReceiptURl = request.ReceiptURL,
                TotalPrice = request.TotalPrice,
                OrderDate = request.OrderDate,
            };
            var id = CreateDb(query, values);
            return id;
        }

        public IEnumerable<Order> GetAllByUserEmail(string email)
        {
            var query = @"SELECT * 
                          FROM Orders
                          WHERE UserEmail = @UserEmail";
            var values = new
            {
                UserEmail = email,
            };
            using var connection = _dbContext.CreateConnection();
            var response = connection.Query<Order>(query, values);
            return response;

        }

        public Order GetByTransactionId(string transactionId)
        {
            var query = @"SELECT * 
                          FROM Orders
                          WHERE TransactionId = @TransactionId
                          ";
            var values = new
            {
                TransactionId = transactionId
            };

            var response = GetByIdDb(query, values);
            return response;
        }
    }
}

