using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ResponseModel> SaveAuthor(NewAuthorRequest request);
        Task<AllAuthorsResponse> GetAllAuthors();
        Task<AllAuthorsResponse> GetByBookId(int bookId);
        Task<AuthorDto> GetDtoById(int id);
        Task<ResponseModel> EditAuthor(EditAuthorRequest request);
        Task<ResponseModel> DeleteAuthor(int id);
    }
}
