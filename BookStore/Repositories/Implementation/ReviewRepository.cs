using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(BookStoreContext context) : base(context)
        {

        }

        public async Task<List<Review>> GetByBookId(int bookId)
        {
            return await _context.Review.Include(r => r.Book).Where(r => r.Book.Id == bookId).ToListAsync();
        }

        public async Task<Review> GetByBookIdAndUsername(int bookId, string username)
        {
            return await _context.Review.Include(r => r.Book).FirstOrDefaultAsync(r => r.Book.Id == bookId && r.Username == username);
        }
    }
}
