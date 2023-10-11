using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.Loan
{
    public class LoanAccount
    {
        public string CustomerID { get; set; }
        public string FullNames { get; set; }
        public List<LoanAccDetails> accDetails { get; set; }
    }


    public class LoanAccDetails
    {
        public string AccountNumber { get; set; }
        public string LoanAccountName { get; set; }
        public string LoanCode { get; set; }
        public string LoanAmount { get; set; }
        public string Status { get; set; }
    }

    public class DisbursmentDetails
    {
        public string CustomerID { get; set; }
        public string CustomerN { get; set; }
       
        public decimal LoanAmount { get; set; }
        public string GlAccNo { get; set; }
        public string IntrstGlAccNo { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string LoanCode { get; set; }
        [Required]
        public string InterestRate { get; set; }
        public string PhoneNumber { get; set; }
        public string Tenor { get; set; }
        public string AccountName { get; set; }
        [Required]
        public string PrincipalInterestAmount { get; set; }

        public string TotalInterest { get; set; }
        public string InterestPerDuration { get; set; }
        [Required]
        public string PrincipalInterestAmountPerDuration { get; set; }
        [Required]
        public string RelationshipOfficerID { get; set; }
        
    }

    public class AccountTransactions
    {
        public string AccountNumber { get; set; }
        public string FullNames { get; set; }
        public string CurrentBalance { get; set; }
        public List<AccountHistorys> History { get; set; }
    }

    public class AccountHistorys
    {
        public string TransReference { get; set; }
        public string TransDiscription { get; set; }
        public string TransCode { get; set; }
        public string TransNaration { get; set; }
        public string TransDate { get; set; }
        public string Cr_Dr { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
    }

    public class RepyTransScherch
    {
       
        
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public string GLAccount { get; set; }
        public string Naration { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountCode { get; set; }
        public bool SMS { get; set; }
        public string AccountNumber1 { get; set; }
        public string InterestRate { get; set; }
        [Required]
        public string PrincipalAmount { get; set; }
        [Required]
        public string PrincipalInterestAmount { get; set; }
        
        public string TotalInterest { get; set; }
     
    }
}
