using System;
using System.ComponentModel.DataAnnotations;

namespace ManageStores.Infrastructure.ViewModels
{
    public class AccountViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }
    }
}
