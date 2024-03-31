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

namespace EcommerceBackend.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public static Users user = new Users();
        private readonly IConfiguration _congfiguration;
        private readonly IAccountServices _accountServices;

        public AccountController(IConfiguration configuration, IAccountServices accountServices)
        {
            _congfiguration = configuration;
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


        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _congfiguration.GetSection("AppSettings:Token").Value!
                ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
         
            var token = new JwtSecurityToken(
                 claims : claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials : creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }

}
