using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<Author>> GetAuthorsByIds(int[] ids);
        Task<List<Author>> GetAuthorsByBookId(int id);
        Task<Author> GetAuthorByIdWithIncludes(int id);
        Task<List<Author>> GetByNameContains(string query);
    }
}
