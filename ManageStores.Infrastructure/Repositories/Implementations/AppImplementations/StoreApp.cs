using ManageStores.Infrastructure.Repositories.Interfaces.Store;
using ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Repositories.Implementations.AppImplementations
{
    public class StoreApp : IStoreApp
    {
        private IStore _store;
        public StoreApp(IStore store)
        {
            _store = store;
        }
        public async Task Add(Core.Models.Store entity)
        {
            await _store.Add(entity);
        }

        public async Task Delete(Core.Models.Store entity)
        {
            await _store.Delete(entity);
        }

        public IQueryable<Core.Models.Store> GetAll()
        {
            return _store.GetAll();
        }

        public Task<List<Core.Models.Store>> GetAllAsyn()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Core.Models.Store> GetAllIncluding(Expression<Func<Core.Models.Store, object>>[] includeProperties)
        {

            return _store.GetAllIncluding(includeProperties);
        }

        public async Task<Core.Models.Store> GetBy(Guid id)
        {
            return await _store.GetBy(id);
        }

        public async Task<Core.Models.Store> Update(Core.Models.Store entity, object key)
        {
            return await _store.Update(entity, key);
        }
    }
}

