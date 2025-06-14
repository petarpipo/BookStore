using BookStore.Models.Dto;

namespace BookStore.Models.Responses
{
    public class GenreResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
