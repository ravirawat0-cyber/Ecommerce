using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DbContext _dbcontext;

        public CartRepository(DbContext dbContext)
        {
                _dbcontext = dbContext;
        }

        public IEnumerable<UserItems> GetItemsByUserId(int userId)
        {
            var query = @"
            SELECT p.Id AS ProductId, p.Name AS ProductName, p.Price AS ProductPrice, ci.Quantity
            FROM Carts c
            JOIN CartItems ci ON c.Id = ci.CartId
            JOIN Products p ON ci.ProductId = p.Id
            WHERE c.UserId = @UserId";

            var value = new { UserId = userId };
            using var connection = _dbcontext.CreateConnection();
            return connection.Query<UserItems>(query, value);
        }
    }
}
