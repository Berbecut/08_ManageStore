using ManageStores.Infrastructure.DataAccess;
using ManageStores.Infrastructure.Repositories.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Repositories.Implementations.Generic
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptionsBuilder<ManageStoresDatabaseContext> _optionsBuilder;
        private ManageStoresDatabaseContext _context;
        private bool disposed = false;
        public GenericRepository()
        {
            _optionsBuilder = new DbContextOptionsBuilder<ManageStoresDatabaseContext>();
            _context = new ManageStoresDatabaseContext(_optionsBuilder.Options);
        }
        public async Task Add(T entity)
        {
            if (_context != null)
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<T> GetBy(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Update(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

            }

            return exist;
        }
        public async Task<List<T>> GetAllAsyn()
        {
            return await _context.Set<T>().ToListAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)

                _context.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
