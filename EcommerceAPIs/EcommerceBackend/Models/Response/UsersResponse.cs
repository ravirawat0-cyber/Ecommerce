namespace EcommerceBackend.Models.Response
{
    public class UsersResponses
    {
        public class User
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
        }

        public class Token
        {
            public string Jwt { get; set; }
        }


        public class Data
        {
            public User User { get; set; }
            public Token Token { get; set; }
            public CartResponse Cart { get; set; } = new CartResponse();
            public WishlistResponse Wishlist { get; set; } = new WishlistResponse();
        }

        public class UserResponse
        {
            public Data Data { get; set; }
            public string StatusMessage { get; set; }
        }
    }
    public class PasswordResponse
    {
        public string Message { get; set; }
    }
}
