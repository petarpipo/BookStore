using BookStore.Models.Dto;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<ResponseModel> SaveBook(NewBookRequest request);

        Task<BooksResponse> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);

        Task<BooksResponse> GetBooksByGenreAsync(int id);
        Task<BooksResponse> GetBooksByIds(int[] ids);
        Task<ResponseModel> EditBook(EditBookRequest request);
        Task<ResponseModel> DeleteBook(int bookId);
        Task<BooksResponse> GetTopRatedBooks();
        Task<BooksResponse> GetBestSellerBooks();
        Task<BooksResponse> GetByAuthorId(int id);
        Task<ResponseModel> UpdateOrderCount(CompleteOrderRequest request);
    }
}
