using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        int Create(Category entity);
        void Update(int id, Category entity);
        void Delete(int id);
        IEnumerable<Category> GetAll();

        Category GetById(int id);

        int CheckCategoryByName(string name);
    }
}
