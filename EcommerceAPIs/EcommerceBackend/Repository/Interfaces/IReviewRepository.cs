using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IReviewRepository
    {
        int Create(Review review);
        IEnumerable<Review> GetByProductId(int id);
        Review GetbyProductAndUserId(int userId, int productId);
        void UpdateReviewByUserAndProductId(Review review);
        void DeleteReviewByUserAndProductId(int userId, int productId);
    }
}
