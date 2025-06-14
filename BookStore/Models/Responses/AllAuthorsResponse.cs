using BookStore.Models.Dto;

namespace BookStore.Models.Responses
{
    public class AllAuthorsResponse
    {
        public List<AuthorDto> Authors { get; set; }
    }
}
