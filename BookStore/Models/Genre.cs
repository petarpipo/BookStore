namespace BookStore.Models
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    } 
}
