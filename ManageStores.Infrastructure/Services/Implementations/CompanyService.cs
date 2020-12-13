using ManageStores.Core.Models;
using ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces;
using ManageStores.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyApp _companyApp;

        public CompanyService(ICompanyApp companyApp)
        {
            _companyApp = companyApp;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _companyApp.GetAllIncluding().AsNoTracking().Include(x => x.Stores).ToListAsync();
        }

        public async Task AddCompany(Company Company)
        {
            await _companyApp.Add(Company);
        }

        public async Task<Company> GetCompanyBy(Guid id)
        {
            return await _companyApp.GetBy(id);
        }

        public async Task UpdateCompany(Company companies, Guid id)
        {
            await _companyApp.Update(companies, id);
        }

        public async Task DeleteCompany(Company company)
        {
            await _companyApp.Delete(company);
        }
    }
}
