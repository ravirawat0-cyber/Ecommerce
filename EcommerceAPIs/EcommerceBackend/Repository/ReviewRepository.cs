using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using Review = EcommerceBackend.Models.DBModels.Review;

namespace EcommerceBackend.Repository
{
    public class ReviewRepository:BaseRepository<Review>, IReviewRepository
    {
        private readonly DbContext _dbContext;
        public ReviewRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Review review)
        {
            var query = @"
                    INSERT INTO Reviews (UserId, ProductId, Rating, Comment)
                    VALUES (@UserId, @ProductId, @Rating, @Comment);
                    SELECT SCOPE_IDENTITY()";

            var values = new
            {
                UserId = review.UserId,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Comment = review.Comments,
            };

            var id = CreateDb(query, values);
            return id;
        }

        public IEnumerable<Review> GetByProductId(int id)
        {
            var query = @"
            SELECT *
            FROM Reviews
            WHERE ProductId = @ProductId";

            var values = new
            {
                ProductId = id,
            };

            var response = GetAllByIdDb(query, values);
            return response;
        }

        public Review GetbyProductAndUserId(int userId, int productId)
        {
            var query = @"
            SELECT *
            FROM Reviews
            WHERE ProductId = @ProductId and UserId = @UserId";

            var values = new
            {
                ProductId = productId,
                UserId = userId
            };

            var response = GetByIdDb(query, values);
            return response;
        }

        public void UpdateReviewByUserAndProductId(Review review)
        {
            var query = @"
            UPDATE Reviews
            SET Rating = @Rating, Comment = @Comment
            WHERE ProductId = @ProductId AND UserId = @UserId";

            var values = new
            {
                Rating = review.Rating,
                Comment = review.Comments,
                ProductId = review.ProductId,
                UserId = review.UserId
            };
            UpdateDb(query, values);
        }

        public void DeleteReviewByUserAndProductId(int userId, int productId)
        {
            var query = @"
            DELETE FROM Reviews
            WHERE ProductId = @ProductId AND UserId = @UserId";

            var values = new
            {
                ProductId = productId,
                UserId = userId
            };

            DeleteDb(query, values);
        }

    }
}
