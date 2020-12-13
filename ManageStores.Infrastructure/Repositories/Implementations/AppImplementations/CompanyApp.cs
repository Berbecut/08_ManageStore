using ManageStores.Infrastructure.Repositories.Interfaces.Company;
using ManageStores.Infrastructure.Repositories.Interfaces.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManageStores.Infrastructure.Repositories.Implementations.AppImplementations
{
    public class CompanyApp : ICompanyApp
    {
        private ICompany _company;

        public CompanyApp(ICompany company)
        {
            _company = company;

        }
        public async Task Add(Core.Models.Company entity)
        {
            await _company.Add(entity);
        }


        public async Task Delete(Core.Models.Company entity)
        {
            await _company.Delete(entity);
        }

        public IQueryable<Core.Models.Company> GetAll()
        {
            return _company.GetAll();
        }

        public async Task<List<Core.Models.Company>> GetAllAsyn()
        {

            return await _company.GetAllAsyn();
        }

        public IQueryable<Core.Models.Company> GetAllIncluding(params Expression<Func<Core.Models.Company, object>>[] includeProperties)
        {
            return _company.GetAllIncluding(includeProperties);
        }

        public async Task<Core.Models.Company> GetBy(Guid id)
        {
            return await _company.GetBy(id);
        }

        public async Task<Core.Models.Company> Update(Core.Models.Company entity, object key)
        {
            return await _company.Update(entity, key);
        }
    }
}

