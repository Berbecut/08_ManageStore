using System;
using System.Collections.Generic;
using System.Text;

namespace ManageStores.Infrastructure.DataAccess
{
    public interface IIdentitySeeder
    {
        bool CreateAdminAccountIFEmpty();
    }
}
