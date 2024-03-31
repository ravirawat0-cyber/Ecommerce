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
            var subCategoryRequest = new SubCategory
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId,
       
            };
            var id = _subCategoryRespository.Create(subCategoryRequest);
            return id;
        }

        public void Delete(int id)
        {
            _subCategoryRespository.Delete(id);
        }

        public IEnumerable<SubCategoryResponse> GetAll()
        {
            var subCategoriesList = _subCategoryRespository.GetAll();
            List<SubCategoryResponse> subCategoryResponsesList = new List<SubCategoryResponse>();
            foreach (var subCategory in subCategoriesList) 
            {
                var subCategoryResponse = new SubCategoryResponse
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    ParentCategoryId= subCategory.ParentCategoryId,
                };
                subCategoryResponsesList.Add(subCategoryResponse);
            }
            return subCategoryResponsesList;
        }

        public SubCategoryResponse GetById(int id)
        {
           var subCategory = _subCategoryRespository.GetById(id);
            var subCategoryResponse = new SubCategoryResponse
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                ParentCategoryId = subCategory.ParentCategoryId,

            };
            return subCategoryResponse;
        }

        public void Update(int id, SubCategoryRequest request)
        {
            var subCategory = new SubCategory
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId,
            };
            _subCategoryRespository.Update(id, subCategory);
            
        }
    }
}
