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

        public class Item
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
            public int Quantity { get; set; }

            public string ImageUrl { get; set; }
        }

        public class Cart
        {
            public List<Item> Items { get; set; } 
            public int TotalItems { get; set; }
        }

        public class Data
        {
            public User User { get; set; }
            public Token Token { get; set; }
            public IEnumerable<Cart> Cart { get; set; } = new List<Cart>();
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
