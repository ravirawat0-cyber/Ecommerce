using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("subcategory")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryServices _subcategoryServices;

        public SubCategoryController(ISubCategoryServices subCategoryServices)
        {
            _subcategoryServices = subCategoryServices;
        }

        [HttpPost("add")]
        public IActionResult Create(SubCategoryRequest request)
        {
            int id = _subcategoryServices.Create(request);
            return Ok(id);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var subCategory = _subcategoryServices.GetAll();
            return Ok(subCategory);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _subcategoryServices.Delete(id);
            return Ok();
        }
    }
}
