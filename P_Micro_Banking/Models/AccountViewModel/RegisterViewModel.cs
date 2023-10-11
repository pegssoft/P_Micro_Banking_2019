using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First_Name")]
        public string First_Name { get; set; }
        [Required]
        [Display(Name = "First_Last")]
        public string First_Last { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Branch")]
        public string Branch { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phone_Number")]
        public string Phone_Number { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
