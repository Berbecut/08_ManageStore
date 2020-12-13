using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ManageStores.Core.Models
{
    public partial class Store
    {
        public Guid Id { get; set; }


        [DisplayName("Choose a company from list")]
        public Guid CompanyId { get; set; }


        [DisplayName("Name")]
        [Required(ErrorMessage = "Insert a store name")]
        [StringLength(255)]
        public string Name { get; set; }


        [DisplayName("Address")]
        [Required(ErrorMessage = "Insert an address")]
        [StringLength(255)]
        public string Address { get; set; }


        [DisplayName("City")]
        [Required(ErrorMessage = "Insert a city")]
        [StringLength(255)]
        public string City { get; set; }

        [DisplayName("Zip Code")]
        [Required(ErrorMessage = "Insert a zip code")]
        [StringLength(255)]
        public string Zip { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Insert a country")]
        [StringLength(255)]
        public string Country { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual Company Company { get; set; }

        public class StoreValidator : AbstractValidator<Store>
        {
            public StoreValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.Address).NotEmpty().MaximumLength(512);
                RuleFor(x => x.City).NotEmpty().MaximumLength(512);
                RuleFor(x => x.Zip).NotEmpty().MaximumLength(16);
                RuleFor(x => x.Country).NotEmpty().MaximumLength(50);
            }
        }
    }
}
