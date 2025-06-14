using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookStoreContext context) : base(context)
        {
        }

        public async Task<List<Author>> GetAuthorsByIds(int[] ids)
        {
            return await _context.Author.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<List<Author>> GetAuthorsByBookId(int id)
        {
            return await _context.Author.Where(a => a.Books.Select(b => b.Id).Contains(id)).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdWithIncludes(int id)
        {
            return await _context.Author.Include(a => a.Genres).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetByNameContains(string query)
        {
            return await _context.Author.Where(b => b.Name.ToLower().Contains(query.ToLower()))
                .ToListAsync();
        }
    }
}
