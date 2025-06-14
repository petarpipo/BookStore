using AutoMapper;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public SearchService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, 
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<SearchResponse> Search(SearchRequest request)
        {
            var books = await _bookRepository.GetByNameContains(request.Query);
            var authors = await _authorRepository.GetByNameContains(request.Query);
            var genres =await _genreRepository.GetByNameContains(request.Query);
            var searchDots = new List<SearchDto>();
            books.ForEach(b =>
            {
                var dto = new SearchDto();
                _mapper.Map(b, dto);
                searchDots.Add(dto);
            });
            authors.ForEach(b =>
            {
                var dto = new SearchDto();
                _mapper.Map(b, dto);
                searchDots.Add(dto);
            });
            genres.ForEach(b =>
            {
                var dto = new SearchDto();
                _mapper.Map(b, dto);
                searchDots.Add(dto);
            });
            return new SearchResponse { SearchDtos = searchDots };
        }
    }
}
