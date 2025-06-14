namespace BookStore.Models.Requests
{
    public class ReviewRequest
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public string Username { get; set; }
    }
}
