﻿namespace EcommerceBackend.Models.DBModels
{
    public class Carts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
    }
}
