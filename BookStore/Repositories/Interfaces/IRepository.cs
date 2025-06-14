using BookStore.Models;
using System.Linq.Expressions;

namespace BookStore.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IList<T> List();
        Task<IList<T>> ListAsync();
        IList<T> List(Expression<Func<T, bool>> expression);
        Task<IList<T>> ListAsync(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        Task<IList<T>> GetAllAsync();
        Task<List<T>> GetByIdsAsync(List<int> ids);
        Task InsertMultipleAsync(List<T> entities);
        Task UpdateMultipleAsync(List<T> entities);
        Task DeleteMultipleAsync(List<T> entities);
    }
}
