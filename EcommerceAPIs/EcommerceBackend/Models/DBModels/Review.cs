using static EcommerceBackend.Models.Response.UsersResponses;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models.DBModels
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
