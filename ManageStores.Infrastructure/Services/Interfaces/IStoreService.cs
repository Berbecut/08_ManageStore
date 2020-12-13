using ManageStores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Services.Interfaces
{
    public interface IStoreService
    {
        Task AddStore(Store store);
        Task UpdateStore(Store store, Guid id);
        Task<IEnumerable<Store>> GetStores();
        Task DeleteStore(Store deleteStore);
        Task<Store> GetStoreBy(Guid id);
    }
}






