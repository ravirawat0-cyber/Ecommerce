using System.Security.Claims;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EcommerceBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;


        public OrderController(IOrderServices orderServices, IAccountServices accountServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAll()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _orderServices.GetAllByUserId(int.Parse(userIdClaim));
            return Ok(response);
        }


        [HttpGet("transactionId/{id}")]
        [Authorize]
        public IActionResult GetByTransactionId(string id)
        {
            var response = _orderServices.GetByTransactionId(id);
            return Ok(response);
        }
    }


}
