using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(BookStoreContext context) : base(context)
        {
        }

        public async Task<List<Genre>> GetGenresByIds(int[] ids)
        {
            return await _context.Genre.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<List<Genre>> GetByBookId(int id)
        {
            return await _context.Genre.Where(g => g.Books.Select(b => b.Id).Contains(id)).ToListAsync();
        }

        public async Task<List<Genre>> GetByAuthorId(int id)
        {
            return await _context.Genre.Include(g=>g.Authors).Where(g => g.Authors.Select(b => b.Id).Contains(id)).ToListAsync();
        }
        public async Task<List<Genre>> GetByNameContains(string query)
        {
            return await _context.Genre.Where(b => b.Name.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }
    }
}
