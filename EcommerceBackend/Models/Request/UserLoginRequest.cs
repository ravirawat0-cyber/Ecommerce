using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models.Request
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
