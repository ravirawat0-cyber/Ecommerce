namespace EcommerceBackend.Models.Request
{
    public class ReviewRequest
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
