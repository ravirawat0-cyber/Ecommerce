using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ISubCategoryServices
    {
        int Create(SubCategoryRequest request);
        void Delete(int id);
        SubCategoryResponse GetAll();
        
    }
}
