using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;

namespace EcommerceBackend.Services
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _subCategoryRespository;

        public SubCategoryServices(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRespository = subCategoryRepository;
        }

        public int Create(SubCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("SubCategory name cannot be empty.");

            if (_subCategoryRespository.IsDataExists(request.Name) > 0)
                throw new ArgumentException("The SubCategory already exists.");

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
                throw new ArgumentException("Image Url cannot be empty.");

            var subCategoryRequest = new SubCategory
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId,
                ImageUrl = request.ImageUrl
            };
            var id = _subCategoryRespository.Create(subCategoryRequest);
            return id;
        }

        public void Delete(int id)
        {
            _subCategoryRespository.Delete(id);
        }

        public SubCategoryResponse GetAll()
        {
            var subCategoriesList = _subCategoryRespository.GetAll();
            var subCategoryResponses = new SubCategoryResponse
            {
                Data = subCategoriesList,
                StatusMessage = "Success"
            };

            return subCategoryResponses;
        }
    }
}