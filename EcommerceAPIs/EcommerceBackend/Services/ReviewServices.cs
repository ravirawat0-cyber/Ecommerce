using System.Collections.Immutable;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;
using Review = EcommerceBackend.Models.DBModels.Review;

namespace EcommerceBackend.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IOrderServices _orderServices;
        private readonly IAccountServices _accountServices;

        public ReviewServices(IReviewRepository reviewRepository, 
                              IOrderServices orderServices,
                              IAccountServices accountServices)
        {
            _reviewRepository = reviewRepository;
            _orderServices = orderServices;
            _accountServices = accountServices;
        }

        public int Create(ReviewRequest request , int userId)
        {
            if (string.IsNullOrWhiteSpace(request.Comments))
                throw new ArgumentException("Review cannot be empty");

            var review = new Review()
            {
                UserId = userId,
                ProductId = request.ProductId,
                Rating = request.Rating,
                Comments = request.Comments
            };

            var response = _reviewRepository.Create(review);
            return response;
        }

        public ReviewResponse GetByProductId(int productId)
        {
            var reviews = _reviewRepository.GetByProductId(productId);
            var response = new ReviewResponse
            {
                StatusMessage = "Success",
                Results = reviews.Count(),
                Data = reviews.Select(review => new ReviewRes
                {
                    Id = review.Id,
                    ProductId = review.ProductId,
                    User = _accountServices.GetNameById(review.UserId),
                    Rating = review.Rating,
                    Comments = review.Comments

                }).ToList()
            };
            return response;
        }

        public ReviewResponse GetByProductAndUserId(int userId, int productId)
        {
            var review = _reviewRepository.GetbyProductAndUserId(userId, productId);

            var response = new ReviewResponse
            {
                StatusMessage = "Success",
                Results = 1,
                Data = new List<ReviewRes>
                {

                    new ReviewRes
                    {
                        Id = review.Id,
                        ProductId = review.ProductId,
                        User = _accountServices.GetNameById(review.UserId),
                        Rating = review.Rating,
                        Comments = review.Comments
                    }
                }
            };
            return response;
        }

        public void UpdateReviewByUserAndProductId(int userId, ReviewRequest request)
        {
        
            var review = new Review()
            {
                UserId = userId,
                ProductId = request.ProductId,
                Rating = request.Rating,
                Comments = request.Comments
            };

            _reviewRepository.UpdateReviewByUserAndProductId(review);
        }

        public void DeleteReviewByUserAndProductId(int userId, int productId)
        {
            _reviewRepository.DeleteReviewByUserAndProductId(userId,productId);
        }


    }
}
