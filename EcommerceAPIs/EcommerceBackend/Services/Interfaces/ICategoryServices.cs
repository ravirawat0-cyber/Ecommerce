using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ICategoryServices
    {
        int Create(CategoryRequest request);
        void Delete(int id);
        CategoryResponse GetAll();

        CategoryResponse GetById(int id);
        void Update(int id, CategoryRequest request);
    }
}
