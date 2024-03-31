using EcommerceBackend.Models.DBModels;

namespace EcommerceBackend.Repository.Interfaces
{
    public interface ISubCategoryRepository
    {
        int Create(SubCategory entity);
        IEnumerable<SubCategory> GetAll();
        SubCategory GetById(int id);
        void Delete(int id);    
        void Update(int id, SubCategory entity);


    }
}
