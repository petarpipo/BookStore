namespace BookStore.Models.Dto
{
    public class ReviewDto
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public string CreatedDate { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
    }
}
