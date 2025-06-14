namespace BookStore.Models.Requests
{
    public class NewBookRequest
    {
        public string Name { get; set; }
        public int[] GenreIds { get; set; }
        public int[]  AuthorIds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        public string ImageUrl { get; set; }
        public int InStock { get; set; }
        public float Price { get; set; }
    }
}
