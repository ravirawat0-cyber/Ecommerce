namespace EcommerceBackend.Models.DBModels
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
