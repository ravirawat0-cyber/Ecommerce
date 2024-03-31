using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Routing.Template;
using static EcommerceBackend.Models.Response.UsersResponses;

namespace EcommerceBackend.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _congfiguration;
        private readonly ICartServices _cartServices;

        public AccountServices(IAccountRepository accountRepository, IConfiguration configuration, ICartServices cartServices)
        {
            _accountRepository = accountRepository;
            _congfiguration = configuration;
            _cartServices = cartServices;
        }

        public UserResponse Register(UserRegisterRequest request)
        {
            var existingUser = _accountRepository.GetUserByCredentials(request.UserName, request.Email, request.Mobile);
            if (existingUser != null)
            {
                if (existingUser.UserName == request.UserName)
                {
                    throw new ArgumentException("Username already exists.");
                }
                else if (existingUser.Email == request.Email)
                {
                    throw new ArgumentException("Email already exists.");
                }
                else if (existingUser.Mobile == request.Mobile)
                {
                    throw new ArgumentException("Mobile number already exists.");
                }
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var users = new Users
            {
                Name = request.Name,
                UserName  = request.UserName,
                Email = request.Email,
                Mobile = request.Mobile,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var userId = _accountRepository.Register(users);
            var userDetails = _accountRepository.GetById(userId);

            var userReponse = new UserResponse
            {
                Data = new Data
                {
                    User = new User
                    {
                        Id = userId,
                        Username = userDetails.UserName,
                        Email = userDetails.Email,
                        Name = userDetails.Name,
                        Mobile = userDetails.Mobile
                    },
                    Token = new Token
                    {
                        Jwt = CreateToken(userDetails)
                    }
                }
            };

            return userReponse;
        }

        public UserResponse UserLogin(UserLoginRequest request)
        {
            var userDetails = _accountRepository.GetUserDetailByUserName(request.UserName);
            if (userDetails == null)
            {
                throw new ArgumentException("User does not exist.Please register.");
            }
            var verfiyPass = VerifyPasswordHash(request.Password, userDetails.PasswordHash, userDetails.PasswordSalt);
            if (!verfiyPass) 
            {
                throw new ArgumentException("Wrong Password");
            }

            var cart = CartDetails(userDetails);


            var userReponse = new UserResponse 
            {
                Data = new Data
                {
                    User = new User
                    {
                        Id = userDetails.Id,
                        Username = userDetails.UserName,
                        Email = userDetails.Email,
                        Name = userDetails.Name,
                        Mobile = userDetails.Mobile
                    },
                    Token = new Token
                    {
                        Jwt = CreateToken(userDetails)
                    },
                    Cart = cart
                }
            };
            return userReponse;
        }

        private Cart CartDetails(Users userDetails)
        {
            var UserItemsFromDb = _cartServices.GetItemsByUserId(userDetails.Id);

            var items = UserItemsFromDb.Select(u => new Item
            {
                ProductId = u.ProductId,
                ProductName = u.ProductName,
                ProductPrice = u.ProductPrice,
                Quantity = u.Quantity,
            }).ToList();

            int totalItems = items.Sum(i => i.Quantity);

            Cart cart = new Cart
            {
                Items = items,
                TotalItems = totalItems
            };
            return cart;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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
                 claims: claims,
                 signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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
