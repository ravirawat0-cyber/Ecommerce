using EcommerceBackend.Helper;
using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;
using System.Xml.Linq;

namespace EcommerceBackend.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IDataHelper _dataHelper;

        public ProductServices(IProductRepository repository, IDataHelper dataHelper)
        {
            _productRepository = repository;
            _dataHelper = dataHelper;
        }

        public int Create(ProductRequest request)
        {
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
            _dataHelper.AddIntoProductSubCategoryTable(id, request.SubCategoryIds);
            return id;
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
            _dataHelper.DeleteFromProductSubCategoryTable(id);
        }

        public ProductResponse GetAll()
        {
            var dbProductResponses = _productRepository.GetProductInfo();
            var productResponse = new ProductResponse
            {
                Products = dbProductResponses
                .GroupBy(p => new { p.ProductID, p.ProductName, p.Description, p.Price })
                .Select(g => new ProductInfo
                {
                    ProductId = g.Key.ProductID,
                    ProductName = g.Key.ProductName,
                    Description = g.Key.Description,
                    Price = g.Key.Price,
                    Categories = g.GroupBy(c => new { c.CategoryName })
                                 .Select(cg => new Categories
                                 {
                                     CategoryNames = cg.Key.CategoryName,
                                     SubCategories = cg.Select(sc => new SubCategoryInfo
                                     {
                                         SubCategoryID = sc.SubCategoryID,
                                         SubCategoryName = sc.SubCategoryName
                                     }).ToList()
                                 }).ToList()
                }).ToList()
            };
            return productResponse;

        }

        public void Update(int id, ProductRequest request)
        {
            var product = new Products
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CompanyName = request.CompanyName,
                Sold = request.Sold,
                IsCustomized = request.IsCustomized,
                KeyFeature = request.KeyFeature,
                IsActive = request.IsActive,
                ImageUrls = request.ImageUrls,
            };
            _productRepository.Update(id, product);
        }
    }
}
