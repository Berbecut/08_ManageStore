using ManageStores.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManageStores.Infrastructure.ViewModels
{
    public class CompaniesViewModel
    {
        public Core.Models.Company Company { get; set; }
        public List<Core.Models.Company> ListOfCompanies { get; set; }

        public IEnumerable<SelectListItem> CompaniesSelectList { get; set; }

        public List<Core.Models.Store> Stores { get; set; }
    }
}

