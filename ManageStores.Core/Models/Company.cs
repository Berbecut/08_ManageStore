using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ManageStores.Core.Models
{
    public partial class Company
    {
        public Company()
        {
            Stores = new HashSet<Store>();
        }

        public Guid Id { get; set; }

        
        [DisplayName("Name")]
        [Required(ErrorMessage = "Insert a company name")]
        [StringLength(255)]
        public string Name { get; set; }


        [DisplayName("Organization Number")]
        [Required(ErrorMessage = "Insert an organization number")]
        public int? OrganizationNumber { get; set; }


        [DisplayName("Notes")]
        public string Notes { get; set; }


        [DisplayName("Stores")]
        public virtual ICollection<Store> Stores { get; set; }


        public class CompanyValidator : AbstractValidator<Company>
        {
            public CompanyValidator()
            {
                RuleFor(x => x.Name).NotNull().MaximumLength(255);
                RuleFor(x => x.OrganizationNumber).NotEmpty();
            }
        }
    }
}

