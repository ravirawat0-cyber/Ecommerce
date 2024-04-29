using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ISubCategoryRepository
    {
        int Create(SubCategory entity);
        IEnumerable<SubCategory> GetAll();
        void Delete(int id);
        IEnumerable<SubCategoryByParentId> GetByParentIds(int id);
        int IsDataExists(string name);
    }
}
