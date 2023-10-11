using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "First_Name")]
        public string First_Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "First_Last")]
        public string First_Last { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Branch")]
        public string Branch { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Phone_Number")]
        public string Phone_Number { get; set; }

    }
}
