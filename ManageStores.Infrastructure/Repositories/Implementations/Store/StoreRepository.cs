using ManageStores.Infrastructure.Repositories.Implementations.Generic;
using ManageStores.Infrastructure.Repositories.Interfaces;
using ManageStores.Infrastructure.Repositories.Interfaces.Store;

namespace ManageStores.Infrastructure.Repositories.Implementations.Store
{
    public class StoreRepository : GenericRepository<Core.Models.Store>, IStore
    {
    }
}
