using BookStore.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface IGenreService
    {
        Task<ResponseModel> SaveGenre(NewGenreRequest request);
        Task<GenresResponse> GetAllGenres();
        Task<List<GenreResponse>> GetByBookId(int bookId);
        Task<List<GenreResponse>> GetByAuthorId(int authorId);
        Task<GenreResponse> GetDtoById(int id);
        Task<ResponseModel> EditGenre(EditGenreRequest request);
        Task<ResponseModel> DeleteGenre(int id);
    }
}
