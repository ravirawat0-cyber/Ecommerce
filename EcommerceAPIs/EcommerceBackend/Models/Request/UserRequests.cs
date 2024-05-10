using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models.Request
{
 
        public class UserRegisterRequest
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class UserLoginRequest
        {
           
            public string Email { get; set; }
            public string Password { get; set; }
        
        }

        public class ForgotPasswordRequest
        {
            public string Email { get; set; }
        }

        public class ResetPasswordRequest
        {
            public byte[] resetToken { get; set; }
            public string NewPassword { get; set; }
        }

        public class UpdatePasswordRequest
        {
            public string resetToken { get; set; }
            public string NewPassword { get; set; }
        }

        public class UserDetailUpdateRequest
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
        }
}
