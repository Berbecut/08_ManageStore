using ManageStores.Core.Models;
using ManageStores.Infrastructure.Services.Interfaces;
using ManageStores.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStores.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var AllCompanies = await _companyService.GetCompanies();
            var vm = new CompaniesViewModel();
            vm.ListOfCompanies = AllCompanies.ToList();

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company companies)
        {
            if (ModelState.IsValid)
            {
                await _companyService.AddCompany(companies);
                return RedirectToAction(nameof(Index));
            }
            return View(companies);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _companyService.GetCompanyBy(id);
            if (companies == null)
            {
                return NotFound();
            }
            return View(companies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company companies, Guid id)
        {
            if (id != companies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _companyService.UpdateCompany(companies, id);

                return RedirectToAction(nameof(Index));
            }

            return View(companies);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyToDelete = await _companyService.GetCompanyBy(id);
            if (companyToDelete == null)
            {
                return NotFound();
            }

            return View(companyToDelete);
        }

        [HttpPost, ActionName("DeleteToConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompanyToConfirm(Guid id)
        {
            var companies = await _companyService.GetCompanyBy(id);
            await _companyService.DeleteCompany(companies);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBulk(string companyIdToDelete)
        {
            if (companyIdToDelete != null)
            {
                Guid[] companiesIdToDeleteArray = Array.ConvertAll(companyIdToDelete.Split(','), Guid.Parse);

                foreach (var item in companiesIdToDeleteArray)
                {
                    var companyToDelete = await _companyService.GetCompanyBy(item);
                    await _companyService.DeleteCompany(companyToDelete);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

