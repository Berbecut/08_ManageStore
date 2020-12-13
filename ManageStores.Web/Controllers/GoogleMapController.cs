using ManageStores.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ManageStores.Web.Controllers
{
    public class GoogleMapController : Controller
    {
        private readonly ManageStoresDatabaseContext _context;

        public GoogleMapController(ManageStoresDatabaseContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            ViewBag.ListOfDropdown = _context.Stores.ToList();
            return View();
        }
    }
}


