namespace BookStore.Models
{
    public class Author : Entity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public List<Book> Books { get; set ; }
        public List<Genre> Genres { get; set; }
    }
}
