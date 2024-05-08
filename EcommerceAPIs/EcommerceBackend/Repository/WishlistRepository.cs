using Dapper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Repository.Interfaces;

namespace EcommerceBackend.Repository
{
    public class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
    {
        private readonly DbContext _dbcontext;

        public WishlistRepository(DbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public IEnumerable<Wishlist> GetItemsByUserId(int userId)
        {
            var query = @"
                         SELECT p.Id AS ProductId,
                             p.Name AS ProductName, 
                             p.Price AS ProductPrice,
                             p.CoverImage as ProductImage
                         FROM Wishlists w
                         JOIN Products p ON w.ProductId = p.Id
                         WHERE w.UserId = @UserId";
            var value = new { UserId = userId };
            using var connection = _dbcontext.CreateConnection();
            var response = connection.Query<Wishlist>(query, value);
            return response;
        }

        public int AddItemsToWishlist(WishlistRequest request, int userId)
        {
            var query = @"INSERT INTO Wishlists
                         (UserId, ProductId)
                         VALUES (@UserId, @ProductId)
                         SELECT SCOPE_IDENTITY()";

            var value = new
            {
                UserId = userId,
                ProductId = request.ProductId,
            };
            var id = CreateDb(query, value);
            return id;
        }


        public void DeleteWishlistItem(int productId, int userId)
        {
            var query = @"
                        DELETE FROM Wishlists
                        WHERE ProductId = @ProductId
                        AND UserId = @UserId";
            var values = new { ProductId = productId, UserId = userId };
            DeleteDb(query, values);
        }

        public bool CheckProductWithUserExist(int productId, int userId)
        {
            var query = @"SELECT COUNT(*)
                          FROM Wishlists
                          WHERE UserId = @UserId AND ProductId = @ProductId";
            var values = new
            {
                UserId = userId,
                ProductId = productId,
            };
            using var connection = _dbcontext.CreateConnection();
            var response = connection.ExecuteScalar<int>(query, values);
            return response > 0;
        }

    }
}
