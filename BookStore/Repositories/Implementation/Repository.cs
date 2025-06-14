using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public BookStoreContext _context;

        public Repository(BookStoreContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public IList<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IList<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IList<T> List(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public async Task<IList<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public async Task<List<T>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Set<T>().Where(t => ids.Contains(t.Id)).ToListAsync();
        }

        public async Task UpdateMultipleAsync(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMultipleAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMultipleAsync(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
