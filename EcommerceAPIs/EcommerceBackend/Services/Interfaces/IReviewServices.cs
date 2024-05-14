using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IReviewServices
    {
        int Create(ReviewRequest request, int userId);
        ReviewResponse GetByProductId(int productId);
        ReviewResponse GetByProductAndUserId(int userId, int productId);
        void UpdateReviewByUserAndProductId(int userId, ReviewRequest request);
        void DeleteReviewByUserAndProductId(int userId, int productId);
    }
}
