using EcommerceBackend.Models.Request;
using EcommerceBackend.Models.Response;
using static EcommerceBackend.Models.Response.UsersResponses;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IAccountServices
    {
        UserResponse Register(UserRegisterRequest request);
        UserResponse UserLogin(UserLoginRequest request);
        UserResponse GetByUserId(string userId);
        PasswordResponse ForgotUserPassword(ForgotPasswordRequest request);
        PasswordResponse UpdatePassword(UpdatePasswordRequest request);
        void UpdateUserDetails(string userId, UserDetailUpdateRequest request);
    }
}
