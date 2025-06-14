using AutoMapper;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using BookStore.Repositories.Implementation;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
        private readonly IGenreRepository _genreRepository;

        public AuthorService(IAuthorRepository authorRepository, 
            IMapper mapper, 
            IGenreService genreService, 
            IGenreRepository genreRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _genreService = genreService;
            _genreRepository = genreRepository;
        }

        public async Task<ResponseModel> SaveAuthor(NewAuthorRequest request)
        {
            var author = new Author();
            var response = new ResponseModel(true)
            {
                Message = "Author saved successfully"
            };
            try
            {
                _mapper.Map(request, author);
                await _authorRepository.InsertAsync(author);
            }
            catch
            {
                response.Message = "Error when saving Author";
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseModel> EditAuthor(EditAuthorRequest request)
        {
            var response = new ResponseModel(true)
            {
                Message = "Author saved successfully"
            };
            var author = await _authorRepository.GetAuthorByIdWithIncludes(request.Id);
            try
            {
                _mapper.Map(request, author);
                author.Genres = await _genreRepository.GetGenresByIds(request.GenreIds);
                await _authorRepository.UpdateAsync(author);
            }
            catch
            {
                response.Message = "Error when saving Author";
                response.Success = false;
            }

            return response;
        }

        public async Task<AllAuthorsResponse> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorsDtoList = new List<AuthorDto>();
            foreach (var author in authors)
            {
                var authorDto = new AuthorDto();
                _mapper.Map(author, authorDto);
                authorDto.Genres = await _genreService.GetByAuthorId(author.Id);
                authorsDtoList.Add(authorDto);
            }

            return new AllAuthorsResponse
            {
                Authors = authorsDtoList
            };
        }

        public async Task<AllAuthorsResponse> GetByBookId(int bookId)
        {
            var authors = await _authorRepository.GetAuthorsByBookId(bookId);
            var authorsDtoList = new List<AuthorDto>();
            foreach (var author in authors)
            {
                var authorDto = new AuthorDto();
                _mapper.Map(author, authorDto);
                authorsDtoList.Add(authorDto);
            }

            return new AllAuthorsResponse
            {
                Authors = authorsDtoList
            };
        }

        public async Task<AuthorDto> GetDtoById(int id)
        {
            var authorDto = new AuthorDto();
            var author = await _authorRepository.GetByIdAsync(id);
            _mapper.Map(author, authorDto);
            authorDto.Genres = await _genreService.GetByAuthorId(author.Id);
            return authorDto;
        }

        public async Task<ResponseModel> DeleteAuthor(int id)
        {
            var responseModel = new ResponseModel(false);
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                await this._authorRepository.DeleteAsync(author);
                responseModel.Success = true;
                responseModel.Message = "Author deleted successfully";
            }
            catch (Exception ex)
            {
                responseModel.Message = "Something went wrong";
            }
            return responseModel;

        }
    }
}
