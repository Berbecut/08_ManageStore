using ManageStores.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Services.Interfaces
{
    public interface ICompanyService
    {
        Task AddCompany(Company comp);
        Task UpdateCompany(Company companies, Guid id);
        Task<IEnumerable<Company>> GetCompanies();
        Task DeleteCompany(Company deleteCompany);
        Task<Company> GetCompanyBy(Guid id);
    }
}




