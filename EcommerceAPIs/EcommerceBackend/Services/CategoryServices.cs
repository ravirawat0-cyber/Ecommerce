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
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name cannot be empty.");

            if (_categoryRepository.CheckCategoryByName(request.Name) > 0)
            {
                throw new ArgumentException("The Category is already exists.");
            }
            
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

        public CategoryWithSubCategoriesResponse GetCategoryWithSubCategories()
        {
            var dbCategories = _categoryRepository.GetCategoryWithSubCategory();
            if (dbCategories == null)
                throw new KeyNotFoundException("Data not found.");

            var categoryInfoList = dbCategories
                .GroupBy(dbCategory => dbCategory.CategoryId)
                .Select(group => new CategoryInfoResponse
                {
                    Id = group.Key,
                    Category = group.First().Category,
                    Subcategories = group.Select(subCategory => new SubCategoryInfoResponse
                    {
                        Id = subCategory.SubCategoryId,
                        Name = subCategory.SubCategoryName,
                        ImageUrl = subCategory.SubCategoryImageUrl
                    })
                })
                .ToList();

            return new CategoryWithSubCategoriesResponse
            {
                Data = categoryInfoList,
                StatusMessage = "Success."
            };

        }

    }
}
