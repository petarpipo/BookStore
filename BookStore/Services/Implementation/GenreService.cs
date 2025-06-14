using System.Runtime.CompilerServices;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository,
            IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel> SaveGenre(NewGenreRequest request)
        {
            var response = new ResponseModel(true) { Message = "Genre saved successfully" };
            try
            {
                var genre = new Genre();
                _mapper.Map(request, genre);
                await _genreRepository.InsertAsync(genre);
            }
            catch
            {
                response.Success = false;
                response.Message = "Error saving Genre";
            }

            return response;
        }

        public async Task<GenresResponse> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreResponseList = new List<GenreResponse>();
            foreach (var genre in genres)
            {
                var genreResponse = new GenreResponse();
                _mapper.Map(genre, genreResponse);
                genreResponseList.Add(genreResponse);
            }

            return new GenresResponse { Genres = genreResponseList };
        }

        public async Task<List<GenreResponse>> GetByBookId(int bookId)
        {
            var genres = await _genreRepository.GetByBookId(bookId);
            var genreList = new List<GenreResponse>();
            foreach (var genre in genres)
            {
                var genreResponse = new GenreResponse();
                _mapper.Map(genre, genreResponse);
                genreList.Add(genreResponse);
            }

            return genreList;
        }

        public async Task<List<GenreResponse>> GetByAuthorId(int authorId)
        {
            var genres = await _genreRepository.GetByAuthorId(authorId);
            var genreList = new List<GenreResponse>();
            foreach (var genre in genres)
            {
                var genreResponse = new GenreResponse();
                _mapper.Map(genre, genreResponse);
                genreList.Add(genreResponse);
            }

            return genreList;
        }

        public async Task<GenreResponse> GetDtoById(int id)
        {
            var response = new GenreResponse();
            var genre = await _genreRepository.GetByIdAsync(id);
            _mapper.Map(genre, response);
            return response;
        }

        public async Task<ResponseModel> EditGenre(EditGenreRequest request)
        {
            var response = new ResponseModel(true)
            {
                Message = "Genre saved successfully"
            };
            var genre = await _genreRepository.GetByIdAsync(request.Id);
            try
            {
                _mapper.Map(request, genre);
                await _genreRepository.UpdateAsync(genre);
            }
            catch
            {
                response.Message = "Error when saving Genre";
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseModel> DeleteGenre(int id)
        {
            var responseModel = new ResponseModel(false);
            try
            {
                var genre = await _genreRepository.GetByIdAsync(id);
                await _genreRepository.DeleteAsync(genre);
                responseModel.Success = true;
                responseModel.Message = "Genre deleted successfully";
            }
            catch
            {
                responseModel.Message = "Something went wrong";
            }
            return responseModel;
        }
    }
}
