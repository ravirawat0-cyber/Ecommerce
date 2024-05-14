namespace EcommerceBackend.Models.Response
{
    public class UserRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
     //  public string UserImage { get; set; }
    }

    public class ReviewRes
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public UserRes User { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }

    public class ReviewResponse
    {
        public string StatusMessage { get; set; }
        public int Results { get; set; }
        public List<ReviewRes> Data { get; set; }
    }
}
