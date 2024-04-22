using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("category")]
    [ApiController]
    // [EnableCors("AllowSpecificOrigin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpPost("add")]
        public IActionResult Create(CategoryRequest categoryRequest)
        {
            int id = _categoryServices.Create(categoryRequest);
            return Ok(id);
        }

        [HttpGet("")]
        public IActionResult GetAll() {
            var category = _categoryServices.GetAll();
            return Ok(category);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) {
            var category = _categoryServices.GetById(id);
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CategoryRequest categoryRequest)
        {
            _categoryServices.Update(id, categoryRequest);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryServices.Delete(id);
            return Ok();    
        }
        
    }
}
