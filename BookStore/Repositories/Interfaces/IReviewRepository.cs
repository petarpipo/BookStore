using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetByBookId(int bookId);
        Task<Review> GetByBookIdAndUsername(int bookId, string username);
    }
}
