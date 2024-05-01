using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpPost("add")]
        public IActionResult Create(ProductRequest request)
        {
            int id = _productServices.Create(request);
                return Ok(id);
        }

        [HttpGet("")]
        public IActionResult GetAll() 
        {
            var response = _productServices.GetAll();
                return Ok(response);
        }

        [HttpGet("subcategory/{id}")]
        public IActionResult GetbySubCategoryId(int id)
        {
            var response = _productServices.GetDetailsBySubCategoryId(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _productServices.GetByProductId(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             _productServices.Delete(id);
             return Ok();
        }


    }
}
