using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetBooksByGenreId(int id);
        Task<List<Book>> GetBooksByIds(int[] ids);
        Task<Book> GetBookByIdWithInclude(int id);
        Task<List<Book>> GetBestSellerBooks();
        Task<List<Book>> GetAllWithReviews();
        Task<List<Book>> GetByAuthorId(int id);
        Task<List<Book>> GetByNameContains(string query);
    }
}
