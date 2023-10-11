using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.Customer
{
    public class CustomerOnboarding
    {
        [Required]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int MaritalStatus { get; set; }
        [Required]
        public int Title { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        public int Country { get; set; }
        [Required]
        public int State { get; set; }
        [Required]
        public int City { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        
        public string NFirstName { get; set; }
        
        public string NLastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string NEmail { get; set; }
        
        [StringLength(11)]
        public string NPhoneNumber { get; set; }
       
        public int Relationship { get; set; }
       
        public string NAddressLine { get; set; }
        
        public int IdentificationType { get; set; }
       
        [StringLength(11)]
        public string IdentificationNumber { get; set; }
        [Required]
        public string RelationshipOfficer { get; set; }
        
        public string AccountName { get; set; }
        
        public string AccountCode { get; set; }
        [Required]
        public int PackageClass { get; set; }
        [Required]
        public string PackageType { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
        [Required]
        public decimal DepositCharge { get; set; }
        [Required]
        public decimal WithdrawerCharge { get; set; }
        public decimal OpeningCharge { get; set; }
        public string GlAccNo { get; set; }
        [Required]
        public string Duration { get; set; }
        
    }

    public class CustomerV
    {
        public string Title { get; set; }
       
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        
        public string DOB { get; set; }

        public string CreatedDate { get; set; }

        public string CustomerId { get; set; }
        
        
        public List<Account> accounts { get; set; }
    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }

        public string AccountCode { get; set; }
        
        public int PackageClass { get; set; }
        
        public int PackageType { get; set; }
    }
}
