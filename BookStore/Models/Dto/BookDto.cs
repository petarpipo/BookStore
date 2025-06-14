using BookStore.Models.Responses;

namespace BookStore.Models.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GenreResponse> Genres { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public string ReleaseDate { get; set; }
        public double ReviewScore { get; set; }
        public string ImageUrl { get; set; }
        public string Overview { get; set; }
        public int InStock { get; set; }
        public float Price { get; set; }
    }
}
