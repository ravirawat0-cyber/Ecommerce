using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface IProductRepository
    {
        int Create(Products entity);

        void Delete(int id);
        void Update(int id, Products entity);
        public IEnumerable<DbProductResponse> GetProductInfo();
    }
}
