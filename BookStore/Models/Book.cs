namespace BookStore.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authros { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Review> Reviews { get; set; }
        public string ImageUrl { get; set; }
        public string Overview { get; set; }
        public int OrderCount { get; set; }
        public int InStock { get; set; }
        public float Price { get; set; }
    }
}
