using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IProductRepository
    {
        int Create(Products entity);

        void Delete(int id);

        public IEnumerable<DbProductResponse> GetProductInfo();

        IEnumerable<ProductDetails> GetProductsDetailsBySubCategoryId(int id);
        Products GetByProductId(int id);
    }
}
