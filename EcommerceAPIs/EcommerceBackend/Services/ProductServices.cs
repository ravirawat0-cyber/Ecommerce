using EcommerceBackend.Helper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;
using System.Xml.Linq;

namespace EcommerceBackend.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IDataHelper _dataHelper;
        private readonly IReviewServices _reviewServices;

        public ProductServices(IProductRepository repository,
            IDataHelper dataHelper, 
            IReviewServices reviewServices)
        {
            _productRepository = repository;
            _dataHelper = dataHelper;
            _reviewServices = reviewServices;
        }

        public int Create(ProductRequest request)
        {
            validateRequest(request);
            var product = new Products
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                SubcategoryId = request.SubCategoryId,
                CompanyName = request.CompanyName,
                KeyFeature = request.KeyFeature,
                CoverImage = request.CoverImage,
                ImageUrls = request.ImageUrls,
            };
            var id = _productRepository.Create(product);
            return id;
        }


        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public ProductResponse GetAll()
        {
            var dbProductResponses = _productRepository.GetProductInfo();

            var productResponse = new ProductResponse
            {
                Products = dbProductResponses.Select(p => new ProductInfo
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CompanyName = p.CompanyName,
                    Sold = p.Sold > 0,
                    Keyfeature = p.KeyFeature,
                    CoverImage = p.CoverImage,
                    ImageUrls = p.ImageUrls,
                    Categories = new Categories
                    {
                        Parentcategory = new CategoryInfo
                        {
                            CategoryId = int.Parse(p.CategoryId),
                            CategoryName = p.CategoryName
                        },
                        Subcategories = new SubCategoryInfo
                        {
                            SubCategoryId = p.SubcategoryId,
                            SubCategoryName = p.SubcategoryName
                        }
                    }
                }).ToList()
            };

            return productResponse;

        }

        public ProductDetailsResponse GetDetailsBySubCategoryId(int id)
        {
            var dbproductDetail = _productRepository.GetProductsDetailsBySubCategoryId(id);
            if (!dbproductDetail.Any())
                throw new KeyNotFoundException("Product details not found for given id.");

            var productList = new ProductDetailsResponse
            {
                Products = dbproductDetail.Select(product =>
                {
                    var ratingInfo = GetRatingInfo(product.Id);
                    return new ProductDetails
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        CoverImage = product.CoverImage,
                        KeyFeature = product.KeyFeature,
                        AverageRating = ratingInfo.AverageRating,
                        TotalRating = ratingInfo.TotalRatings
                    };
                }).ToList(),
                StatusMessage = "Success."
            };
            return productList;
        }



        public ProductProfileResponse GetByProductId(int id)
        {
            var productDetail = _productRepository.GetByProductId(id);
            if (productDetail == null)
                throw new KeyNotFoundException("Product detail not found for given id.");

            var ratingInfo = GetRatingInfo(productDetail.Id);
            productDetail.AverageRating = ratingInfo.AverageRating;
            productDetail.TotalRating = ratingInfo.TotalRatings;

            var productResponse = new ProductProfileResponse
            {
                Products = productDetail,
                StatusMessage = "Success"
            };
            return productResponse;
        }


        private RatingInfo GetRatingInfo(int productId)
        {
            var reviews = _reviewServices.GetByProductId(productId);
            if (reviews.Data == null || !reviews.Data.Any())
            {
                return new RatingInfo
                {
                    AverageRating = 0,
                    TotalRatings = 0
                };
            }

            int averageRating = (int)reviews.Data.Average(r => r.Rating);
            int totalRatings = reviews.Results;

            return new RatingInfo
            {
                AverageRating = averageRating,
                TotalRatings = totalRatings
            };
        }
        private void validateRequest(ProductRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Product name cannot be null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(request.Description))
                throw new ArgumentException("Description cannot be null or whitespace.");
            
            if (request.Price <= 0)
                throw new ArgumentException("Price must be greater than 0.");
            
            if(string.IsNullOrWhiteSpace(request.CompanyName))
                throw new ArgumentException("Company name cannot be null or whitespace.");
            
            if(string.IsNullOrWhiteSpace(request.KeyFeature))
                throw new ArgumentException("Key feature cannot be null or whitespace.");

            if(string.IsNullOrWhiteSpace(request.ImageUrls))
                throw new ArgumentException("Image urls cannot be null or whitespace.");
            
            if(string.IsNullOrWhiteSpace(request.CoverImage))
                throw new ArgumentException("Image urls cannot be null or whitespace.");
        }

    }
}
