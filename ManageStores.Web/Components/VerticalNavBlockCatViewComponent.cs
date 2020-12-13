using ManageStores.Infrastructure.DataAccess;
using ManageStores.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStores.Web.Components
{
    public class VerticalNavBlockCatViewComponent : ViewComponent
    {
        private ManageStoresDatabaseContext ctx;
        public VerticalNavBlockCatViewComponent(ManageStoresDatabaseContext context)
        {
            ctx = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new CompaniesViewModel();

            var listOfCompanies = await ctx.Companies.Include(x => x.Stores).ToListAsync();

            vm.ListOfCompanies = listOfCompanies;

            return View(vm);
        }
    }
}
