using EcommerceBackend.Models.DBModels;
using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using EcommerceBackend.Repository.Interfaces;
using EcommerceBackend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static EcommerceBackend.Models.Response.UsersResponses;
using User = EcommerceBackend.Models.Response.UsersResponses.User;

namespace EcommerceBackend.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWishlistServices _wishlistServices;
        private readonly IConfiguration _congfiguration;
        private readonly ICartServices _cartServices;

        public AccountServices(IAccountRepository accountRepository,IWishlistServices wishlistServices, IConfiguration configuration, ICartServices cartServices)
        {
            _accountRepository = accountRepository;
            _wishlistServices = wishlistServices;
            _congfiguration = configuration;
            _cartServices = cartServices;

        }

        public UserResponse Register(UserRegisterRequest request)
        {
            var existingUser = _accountRepository.GetUserByCredentials( request.Email, request.Mobile);
            if (existingUser != null)
            {
                 if (existingUser.Email == request.Email)
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
                Email = request.Email,
                Mobile = request.Mobile,
                Address = request.Address,
                JoinedDate = DateTime.Now,
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
                        Email = userDetails.Email,
                        Name = userDetails.Name,
                        Mobile = userDetails.Mobile,
                        Address = userDetails.Address,
                        JoinedDate = userDetails.JoinedDate,

                    },
                    Token = new Token
                    {
                        Jwt = CreateToken(userDetails)
                    }
                },
                StatusMessage = "Success",
            };

            return userResponse;
        }

        public UserResponse UserLogin(UserLoginRequest request)
        {
            var userDetails = _accountRepository.GetUserDetailByColumnName("Email",request.Email);
            if (userDetails == null)
            {
                throw new ArgumentException("Email does not exist.Please register.");
            }
            var verifyPass = VerifyPasswordHash(request.Password, userDetails.PasswordHash, userDetails.PasswordSalt);
            if (!verifyPass) 
            {
                throw new ArgumentException("Wrong Password");
            }

            var cartDetail = _cartServices.GetItemsByUserId(userDetails.Id);
            var wishListDetail = _wishlistServices.GetWishlistItemsByUserId(userDetails.Id);

            var userResponse = new UserResponse 
            {
                Data = new Data
                {
                    User = new User
                    {
                        Id = userDetails.Id,
                        Email = userDetails.Email,
                        Name = userDetails.Name,
                        Mobile = userDetails.Mobile,
                        Address = userDetails.Address,
                        JoinedDate = userDetails.JoinedDate
                    },
                    Token = new Token
                    {
                        Jwt = CreateToken(userDetails)
                    },
                    Cart = cartDetail,
                    Wishlist = wishListDetail
                },
                StatusMessage = "Success."
            };
            return userResponse;
        }


        public UserResponse GetByUserId(string userId)
        {
            var userDetail = _accountRepository.GetById(Convert.ToInt32(userId));
            var cartDetail = _cartServices.GetItemsByUserId(userDetail.Id);
            var wishlistDetail = _wishlistServices.GetWishlistItemsByUserId(userDetail.Id);

            var userResponse = new UserResponse
            {
                Data = new Data
                {
                    User = new User
                    {
                        Id = userDetail.Id,
                        Email = userDetail.Email,
                        Name = userDetail.Name,
                        Mobile = userDetail.Mobile,
                        Address = userDetail.Address,
                        JoinedDate = userDetail.JoinedDate
                    },
                    Token = new Token
                    {
                        Jwt = CreateToken(userDetail)
                    },
                    Cart = cartDetail,
                    Wishlist = wishlistDetail
                },
                StatusMessage = "Success."
            };
            return userResponse;
        }

        public UserRes GetNameById(int id)
        {
            return _accountRepository.GetNameById(id);
        }
        public string GetEmailByUserId(int userId)
        {
            var response = _accountRepository.GetUserEmailbyId(userId);
            return response;
        }

        public int GetUserIdByEmail(string email)
        {
            var response = _accountRepository.GetUserIdbyEmail(email);
            return response;
        }

        public void UpdateUserDetails(string userId, UserDetailUpdateRequest request)
        {
            _accountRepository.UpdateUserDetails(int.Parse(userId), request);
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
            var resetToken = Encoding.UTF8.GetBytes(request.resetToken);
            var userId = _accountRepository.GetUserIdByToken(resetToken);
            if (userId == null)
            {
                throw new ArgumentException("Invalid or expired reset token.");
            }

            var userDetails = _accountRepository.GetById(userId);
            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            userDetails.PasswordHash = passwordHash;
            userDetails.PasswordSalt = passwordSalt;
            _accountRepository.UpdateUserPassword(userId, userDetails);
            _accountRepository.RemovePasswordResetToken(resetToken);
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

        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private string CreateToken(Users user)
        {
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
             
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.UserData, user.Id.ToString()),
                new Claim(ClaimTypes.Expiration, expiryDate.ToString("o"))
            }; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _congfiguration.GetSection("AppSettings:Token").Value!
                ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                 claims: claims,
                 expires:expiryDate,
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
