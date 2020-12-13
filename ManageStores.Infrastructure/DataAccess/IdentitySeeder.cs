using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManageStores.Infrastructure.DataAccess
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _admin = "florin";
        private const string _password = "florin123";

        private readonly ManageStoresDatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(ManageStoresDatabaseContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool CreateAdminAccountIFEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
                var result = _userManager.CreateAsync(new IdentityUser
                {
                    UserName = _admin,
                    Email = "florin@email.se",
                    EmailConfirmed = true
                }, _password).Result;
            }
            return true;
        }
    }
}
