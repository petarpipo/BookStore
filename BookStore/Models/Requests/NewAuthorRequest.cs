namespace BookStore.Models.Requests
{
    public class NewAuthorRequest
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public int[] GenreIds { get; set; }
    }
}
