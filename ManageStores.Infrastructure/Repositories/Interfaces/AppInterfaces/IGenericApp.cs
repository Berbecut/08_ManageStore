using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces
{
    public interface IGenericApp<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllAsyn();
        Task<T> GetBy(Guid id);
        Task Add(T entity);
        Task<T> Update(T entity, object key);
        Task Delete(T entity);
    }
}
