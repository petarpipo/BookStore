using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context)
        {
        }

        public async Task<List<Book>> GetBooksByGenreId(int id)
        {
            return await _context.Book.Include(g => g.Genres).Where(b => b.Genres.Select(g => g.Id).Contains(id)).Include(b => b.Reviews)
                .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByIds(int[] ids)
        {
            return await _context.Book.Include(b => b.Genres).Include(b => b.Authros).Include(b => b.Reviews).Where(b => ids.Contains(b.Id))
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdWithInclude(int id)
        {
            return await _context.Book.Include(b => b.Genres).Include(b => b.Authros).Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBestSellerBooks()
        {
            return await _context.Book.Include(b => b.Genres).Include(b => b.Authros).Include(b => b.Reviews)
                .OrderByDescending(r => r.OrderCount).Take(10).ToListAsync();
        }

        public async Task<List<Book>> GetAllWithReviews()
        {
            return await _context.Book.Include(b => b.Reviews).ToListAsync();
        }

        public async Task<List<Book>> GetByAuthorId(int id)
        {
            return await _context.Book.Include(b => b.Genres).Include(b => b.Authros).Include(b => b.Reviews)
                .Where(b => b.Authros.Select(a => a.Id).Contains(id)).ToListAsync();
        }

        public async Task<List<Book>> GetByNameContains(string query)
        {
            return await _context.Book.Where(b => b.Name.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }
    }
}
