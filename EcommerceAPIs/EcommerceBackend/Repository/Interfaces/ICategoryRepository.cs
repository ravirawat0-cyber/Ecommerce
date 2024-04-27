using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Response;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        int Create(Category entity);
        void Update(int id, Category entity);
        void Delete(int id);
        IEnumerable<Category> GetAll();

        Category GetById(int id);

        IEnumerable<DbCategoryResponse> GetCategoryWithSubCategory();

        int CheckCategoryByName(string name);
    }
}
