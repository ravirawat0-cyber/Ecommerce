using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public int Create(CategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
            };
            var id = _categoryRepository.Create(category);
            return id;
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }

        public CategoryResponse GetAll()
        {
            var categorylist = _categoryRepository.GetAll();
            var categoryResponse = new CategoryResponse
            {
                Data = categorylist,
                StatusMessage = "Success"

            };
            return categoryResponse;

        }


        public CategoryResponse GetById(int id)
        {
            var category = _categoryRepository.GetById(id); 
            var categoryResponse = new CategoryResponse
            {
                Data = new[]
                {
                    new Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                    }
                },
                StatusMessage = "Success"
            };
            return categoryResponse;
        }

        public void Update(int id, CategoryRequest request)
        {
            var category = new Category { Name = request.Name, };
            _categoryRepository.Update(id, category);
        }
    }
}
