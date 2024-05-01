using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IProductServices
    {
        int Create(ProductRequest request);

        void Delete(int id);
        ProductResponse GetAll();
        ProductDetailsResponse GetDetailsBySubCategoryId(int id);
        ProductProfileResponse GetByProductId(int id);

    }
}
