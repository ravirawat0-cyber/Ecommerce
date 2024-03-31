using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Services.Interfaces
{
    public interface ISubCategoryServices
    {
        int Create(SubCategoryRequest request);
        void Delete(int id);
        IEnumerable<SubCategoryResponse> GetAll();

        SubCategoryResponse GetById(int id);
        void Update(int id, SubCategoryRequest request);
    }
}
