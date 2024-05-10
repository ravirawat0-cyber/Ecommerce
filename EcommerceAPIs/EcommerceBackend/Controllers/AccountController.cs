using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
       
            _accountServices = accountServices;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequest request)
        {
           var userDetails = _accountServices.Register(request);
            return Ok(userDetails);
        }


        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var userDetails = _accountServices.UserLogin(request);
            return Ok(userDetails);
        }

        [HttpGet("")]
        [Authorize]
        public IActionResult getUserDetial()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            var response = _accountServices.GetByUserId(userIdClaim);
            return Ok(response);
        }


        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUserDetails(UserDetailUpdateRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value;
            _accountServices.UpdateUserDetails(userIdClaim, request);
            return Ok();
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequest request)
        {
            var message = _accountServices.ForgotUserPassword(request);
            return Ok(message);
        }


        [HttpPut("update-password")]
        public IActionResult UpdatePassword(UpdatePasswordRequest request)
        {
            var message = _accountServices.UpdatePassword(request);
            return Ok(message);
        }

    }
}
