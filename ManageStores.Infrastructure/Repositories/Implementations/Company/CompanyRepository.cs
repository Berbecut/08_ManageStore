using ManageStores.Infrastructure.Repositories.Implementations.Generic;
using ManageStores.Infrastructure.Repositories.Interfaces;
using ManageStores.Infrastructure.Repositories.Interfaces.Company;

namespace ManageStores.Infrastructure.Repositories.Implementations.Company
{
    public class CompanyRepository : GenericRepository<Core.Models.Company>, ICompany
    {
    }
}
