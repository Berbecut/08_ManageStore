using ManageStores.Core.Models;
using ManageStores.Infrastructure.Services.Interfaces;
using ManageStores.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManageStores.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IStoreService _storService;
        public StoreController(ICompanyService companyService, IStoreService storService)
        {
            _companyService = companyService;
            _storService = storService;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _storService.GetStores();

            var vm = new StoresViewModel();
            vm.AllStores = stores.ToList();

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _companyService.GetCompanies();
            if (model.Count()>0)
            {
                ViewData["CompanyIdToSelect"] = new SelectList(model, "Id", "Name");
            }
            else 
            { 
                ViewData["Warning"] = "To be able to create a store, please create first a company";
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Store stores)
        {
            if (ModelState.IsValid)
            {
                stores.Id = Guid.NewGuid();
                Address address = new Address()
                {
                    StreetAddress = stores.Address,
                    City = stores.City,
                    ZipCode = stores.Zip,
                    Country = stores.Country
                };

                GetCoordinates(address);
                stores.Latitude = address.Position.Latitude;
                stores.Longitude = address.Position.Longitude;

                await _storService.AddStore(stores);

                if (string.IsNullOrEmpty(stores.Latitude) || string.IsNullOrEmpty(stores.Longitude))
                {
                    var model = _companyService.GetCompanies();

                    // Display Id and CompanyName in asp-item in Store/Index.cshtml
                    ViewData["CompanyIdToSelect"] = new SelectList(model.Result, "Id", "Name", stores.CompanyId);
                    ViewData["Position"] = "GPS coordinates could not be identified. Please check again the store address.";

                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stores);
        }

        public IActionResult CreateStoreInCompanyPage(Guid id)
        {
            var stores = new StoresViewModel();
            stores.CompanyId = id;
            var model = _companyService.GetCompanyBy(id);
            ViewData["CompanyIdToSelect"] = model.Result.Name;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreInCompanyPage(Guid id, Store stores)
        {
            stores.CompanyId = id;
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                stores.Id = Guid.NewGuid();

                Address address = new Address()
                {
                    StreetAddress = stores.Address,
                    City = stores.City,
                    ZipCode = stores.Zip,
                    Country = stores.Country
                };

                GetCoordinates(address);
                stores.Latitude = address.Position.Latitude;
                stores.Longitude = address.Position.Longitude;

                await _storService.AddStore(stores);

                if (string.IsNullOrEmpty(stores.Latitude) || string.IsNullOrEmpty(stores.Longitude))
                {
                    ViewData["Position"] = "GPS coordinates could not be identified. Please check again the store address";
                    var model = _companyService.GetCompanyBy(id);
                    ViewData["CompanyIdToSelect"] = model.Result.Name;
                    return View();
                }
            }
            return RedirectToAction("Index", "Company");
        }

        private void GetCoordinates(Address address)
        {
            var geoLocation = new GoogleGeoLocation();
            geoLocation.GetCoordinates(address);
            address.Position.Latitude = geoLocation.Latitude;
            address.Position.Longitude = geoLocation.Longitude;
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _storService.GetStoreBy(id);
            if (stores == null)
            {
                return NotFound();
            }
            var model = _companyService.GetCompanies();
            ViewData["CompanyIdToSelect"] = new SelectList(model.Result, "Id", "Name", stores.CompanyId);
            return View(stores);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Store store)
        {
            if (id != store.Id) // Check id from (asp-route-id="@store.Id) from Store/Index.cshtml with Id stored in Store table in Db
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Address address = new Address()
                {
                    StreetAddress = store.Address,
                    City = store.City,
                    ZipCode = store.Zip,
                    Country = store.Country
                };

                GetCoordinates(address);
                store.Latitude = address.Position.Latitude;
                store.Longitude = address.Position.Longitude;
                await _storService.UpdateStore(store, id);


                if (string.IsNullOrEmpty(store.Latitude) || string.IsNullOrEmpty(store.Longitude))
                {
                    ViewData["Position"] = "GPS coordinates could not be identified. Please check again the store address";
                    var model = _companyService.GetCompanies();
                    ViewData["CompanyIdToSelect"] = new SelectList(model.Result, "Id", "Name", store.CompanyId);
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _storService.GetStoreBy(id);

            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        [HttpPost, ActionName("DeleteToConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStoreToConfirm(Guid id)
        {
            var stores = await _storService.GetStoreBy(id);
            await _storService.DeleteStore(stores);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBulk(string storeIdToDelete, Guid Companyid)
        {
            if (storeIdToDelete != null)
            {
                Guid[] storesIdToDeleteArray = Array.ConvertAll(storeIdToDelete.Split(','), Guid.Parse);

                foreach (var item in storesIdToDeleteArray)
                {
                    var storeToDelete = await _storService.GetStoreBy(item);
                    await _storService.DeleteStore(storeToDelete);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

