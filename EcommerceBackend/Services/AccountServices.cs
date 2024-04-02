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

            var userResponse = new UserResponse
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

            return userResponse;
        }

        public UserResponse UserLogin(UserLoginRequest request)
        {
            var userDetails = _accountRepository.GetUserDetailByColumnName("UserName",request.UserName);
            if (userDetails == null)
            {
                throw new ArgumentException("User does not exist.Please register.");
            }
            var verifyPass = VerifyPasswordHash(request.Password, userDetails.PasswordHash, userDetails.PasswordSalt);
            if (!verifyPass) 
            {
                throw new ArgumentException("Wrong Password");
            }

            var cart = CartDetails(userDetails);


            var userResponse = new UserResponse 
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
            return userResponse;
        }


        public PasswordResponse ForgotUserPassword(ForgotPasswordRequest request)
        {
            var userDetails = _accountRepository.GetUserDetailByColumnName("Email", request.Email);
            if (userDetails == null)
            {
                throw new ArgumentException("User does not exist. Please register.");
            }

            var resetToken = GeneratePasswordResetToken();

            _accountRepository.AddUserResetToken(userDetails.Id, Encoding.UTF8.GetBytes(resetToken), DateTime.Now.AddDays(1));

            SendPasswordResetLinkToEmail(userDetails.Email, resetToken);

            var resetPasswordResponse = new PasswordResponse
            {
                Message = "A password reset link has been sent to your email."
            };

            return resetPasswordResponse;
        }

        public PasswordResponse UpdatePassword(UpdatePasswordRequest request)
        {
            var userId = _accountRepository.GetUserIdByToken(Encoding.UTF8.GetBytes(request.resetToken));
            if (userId == null)
            {
                throw new ArgumentException("Invalid or expired reset token.");
            }

            var userDetails = _accountRepository.GetById(userId);
            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            userDetails.PasswordHash = passwordHash;
            userDetails.PasswordSalt = passwordSalt;
            _accountRepository.UpdateUserDetails(userId, userDetails);

            return new PasswordResponse
            {
                Message = "Your password has been successfully reset."
            };

        }


        private void SendPasswordResetLinkToEmail(string email, string resetToken)
        {
            var resetLink = $"http://{resetToken}";

            var emailService = new EmailServices();
            emailService.Send(email, "Password Reset",
                $"Please click the following link to reset your password: {resetLink}");
        }

        private string GeneratePasswordResetToken()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randBytes = new byte[32];
            rngCryptoServiceProvider.GetBytes(randBytes);
            return Convert.ToBase64String(randBytes);
        }


        private Cart CartDetails(Users userDetails)
        {
            var userItemsFromDb = _cartServices.GetItemsByUserId(userDetails.Id);

            var items = userItemsFromDb.Select(u => new Item
            {
                ProductId = u.ProductId,
                ProductName = u.ProductName,
                ProductPrice = u.ProductPrice,
                Quantity = u.Quantity,
            }).ToList();

            var totalItems = items.Sum(i => i.Quantity);

            var cart = new Cart
            {
                Items = items,
                TotalItems = totalItems
            };
            return cart;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
            var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

    }
}
