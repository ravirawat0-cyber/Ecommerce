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

        public IEnumerable<CategoryResponse> GetAll()
        {
            var categorylist = _categoryRepository.GetAll();
            List<CategoryResponse> categoryResponseList = new List<CategoryResponse>(); 
            foreach(var category in categorylist)
            {
                var categoryList = new CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                };
                categoryResponseList.Add(categoryList);
            
            }
            return categoryResponseList;    

        }

        public CategoryResponse GetById(int id)
        {
            var category = _categoryRepository.GetById(id); 
            var categoryResponse = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
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
