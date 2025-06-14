using BookStore.Models.Responses;

namespace BookStore.Models.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public List<GenreResponse> Genres { get; set; }
    }
}
