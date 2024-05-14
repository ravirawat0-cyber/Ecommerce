using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services;

namespace EcommerceBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }


        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(ReviewRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _reviewServices.Create(request, int.Parse(userIdClaim));
            return Ok(response);
        }

        [HttpGet("product/{id}")]
        public IActionResult GetByProductId(int id)
        {
            var response = _reviewServices.GetByProductId(id);
            return Ok(response);
        }

        [HttpGet("User/product/{id}")]
        [Authorize]
        public IActionResult GetByUserProductId(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _reviewServices.GetByProductAndUserId(int.Parse(userIdClaim), id);
            return Ok(response);
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult Update(ReviewRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _reviewServices.UpdateReviewByUserAndProductId(int.Parse(userIdClaim), request);
            return Ok();
        }

        [HttpDelete("product/{productId:int}")]
        [Authorize]
        public IActionResult DeleteCartItem(int productId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _reviewServices.DeleteReviewByUserAndProductId(int.Parse(userIdClaim), productId );
            return Ok();
        }
    }
}
