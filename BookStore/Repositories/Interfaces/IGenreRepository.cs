using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<List<Genre>> GetGenresByIds(int[] ids);
        Task<List<Genre>> GetByBookId(int id);
        Task<List<Genre>> GetByAuthorId(int id);
        Task<List<Genre>> GetByNameContains(string query);
    }
}
