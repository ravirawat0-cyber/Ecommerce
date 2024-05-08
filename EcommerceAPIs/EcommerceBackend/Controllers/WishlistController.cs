using System.Security.Claims;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistServices _wishlistServices;

        public WishlistController(IWishlistServices wishlistServices)
        {
            _wishlistServices = wishlistServices;
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(WishlistRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _wishlistServices.AddItemToWishlist(request, userIdClaim);
            return Ok(response);
        }

        [HttpDelete("{productId:int}")]
        [Authorize]
        public IActionResult DeleteCartItem(int productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _wishlistServices.DeleteCartItem(productId, userIdClaim);
            return Ok();
        }
    }
}
