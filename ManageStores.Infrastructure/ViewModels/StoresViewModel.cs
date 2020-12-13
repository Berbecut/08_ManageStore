using ManageStores.Core.Models;
using System;
using System.Collections.Generic;

namespace ManageStores.Infrastructure.ViewModels
{
    public class StoresViewModel
    {
        public List<Store> AllStores { get; set; }

        public Store Store { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}

