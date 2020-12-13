using ManageStores.Core.Models;
using ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces;
using ManageStores.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IStoreApp _storeApp;

        public StoreService(IStoreApp storeApp)
        {
            _storeApp = storeApp;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _storeApp.GetAllIncluding().AsNoTracking().Include(x => x.Company).ToListAsync();
        }

        public async Task AddStore(Store store)
        {
            await _storeApp.Add(store);
        }

        public async Task<Store> GetStoreBy(Guid id)
        {
            return await _storeApp.GetBy(id);
        }

        public async Task UpdateStore(Store store, Guid id)
        {
            await _storeApp.Update(store, id);
        }

        public async Task DeleteStore(Store store)
        {
            await _storeApp.Delete(store);
        }
    }
}



