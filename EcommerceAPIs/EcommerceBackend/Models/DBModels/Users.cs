﻿namespace EcommerceBackend.Models.DBModels
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime JoinedDate { get; set; }
    }


    public class UsersResetToken
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] ResetToken { get; set; }
        public DateTime ExpiryDate { get; set; } 
    }
}
