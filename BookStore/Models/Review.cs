namespace BookStore.Models
{
    public class Review : Entity
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpvoteCount{ get; set; }
        public int DownvoteCount{ get; set; }
        public Book Book { get; set; }
        public string Username { get; set; }
    }
}
