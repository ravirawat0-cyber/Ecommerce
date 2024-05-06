using System.Security.Claims;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
                _cartServices = cartServices;
        }

        
        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(CartRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _cartServices.AddItemToCart(request, userIdClaim);
            return Ok(response);
        }

        [HttpPut()]
        [Authorize]
        public IActionResult Update(CartRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _cartServices.UpdateCart(request, userIdClaim);
            return Ok();
        }
    }
}
