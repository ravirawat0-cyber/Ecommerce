﻿using EcommerceBackend.Models.Request;
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

        [HttpGet("data")]
        public IActionResult GetCategoryWithSubcategory()
        {
            var category = _categoryServices.GetCategoryWithSubCategories();
            return Ok(category);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryServices.Delete(id);
            return Ok();    
        }
        
    }
}
