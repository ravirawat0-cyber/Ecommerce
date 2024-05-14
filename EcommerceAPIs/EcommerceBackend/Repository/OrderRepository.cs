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
                         (UserEmail, UserId, TransactionId, ReceiptURL, TotalPrice, OrderDate)
                         VALUES 
                          (@UserEmail,@UserId, @TransactionId, @ReceiptURL, @TotalPrice, @OrderDate)
                          SELECT SCOPE_IDENTITY()
                         ";
            var values = new
            {
                UserEmail = request.UserEmail,
                UserId = request.UserId,
                TransactionId = request.TransactionId,
                ReceiptURl = request.ReceiptURL,
                TotalPrice = request.TotalPrice,
                OrderDate = request.OrderDate,
            };
            var id = CreateDb(query, values);
            return id;
        }

        public int CreateOrderItems(OrderItem request)
        {
            var query = @"
                         INSERT INTO OrderItems
                         (OrderId, ProductId, Quantity, Price, ProductImage)
                         VALUES 
                          (@OrderId,@ProductId, @Quantity, @Price, @ProductImage)
                          SELECT SCOPE_IDENTITY()
                         ";
            var values = new
            {
                 OrderId = request.OrderId,
                 ProductId = request.ProductId,
                 Quantity = request.Quantity,
                 Price = request.Price,
                 ProductImage = request.ProductImage,
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

        public int hasPurchasedQuery(int userId, int productId)
        {
            var query = @"
                    SELECT COUNT(*)
                    FROM Orders o
                    INNER JOIN OrderItems oi ON o.Id = oi.OrderId
                    WHERE o.UserId = @UserId AND oi.ProductId = @ProductId";

            var values = new { UserId = userId, ProductId = productId };
            var response = GetCountFromDb(query, values);
            return response;
        }
    }
}


