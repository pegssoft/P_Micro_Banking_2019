using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.Deposite
{
    public class DepositeAcc
    {
        public string CustomerID { get; set; }
        public string  FullNames { get; set; }
        public List<AccDetails> accDetails { get; set; }
    }

    public class AccDetails
    {
        public string AccountCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string PackageClass { get; set; }
        public string PackageType { get; set; }
        public string Status { get; set; }
    }

    public class NewAccount
    {
        public string CustomerID { get; set; }
        public string CustomerN { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string AccountCode { get; set; }
        [Required]
        public int PackageClass { get; set; }
        [Required]
        public string PackageType { get; set; }
        [Required]
        public string RelationshipOfficer { get; set; }
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

    public class NewAccounts
    {
        public string CustomerID { get; set; }
        public string CustomerN { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string AccountCode { get; set; }
        [Required]
        public int PackageClass { get; set; }
        [Required]
        public string PackageType { get; set; }
        [Required]
        public string RelationshipOfficer { get; set; }
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
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string NoMonth { get; set; }
        [Required]
        public DateTime MatureDate { get; set; }
        public decimal Amount { get; set; }
    }

    public class AccountTransaction
    {
        public string AccountNumber { get; set; }
        public string FullNames { get; set; }
        public string charge { get; set; }
        public string CurrentBalance { get; set; }
        public List<AccountHistory> History { get; set; }
    }

    public class AccountHistory
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

    public class TransactionSearch
    {
        public string CustomerID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumber1 { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public string AccountClass { get; set; }
        public string InterestRate { get; set; }
        public string OpeningChrge { get; set; }
        public string PhoneNumber { get; set; }
        public string DChargeRate { get; set; }
        public string WChargeRate { get; set; }
        public string Duration { get; set; }
        public string GLAccount { get; set; }
        public string RelationshipOfficerID { get; set; }
        public string BranchCode { get; set; }
        [Required]
        public string Amount { get; set; }
        public string Naration { get; set; }
        public bool SMS { get; set; }
        public string PrewithCharge { get; set; }
        public string AccumulatedNoMonth { get; set; }
        public string StartDate { get; set; }
        public string AcctualNoMonth { get; set; }
        public string Maturitydate { get; set; }
        public string Interest { get; set; }
        public string Total { get; set; }
    }

    public class MaRequest
    {
        public string Startdate { get; set; }
        public string NumberMonth { get; set; }
    }

    public class smsCharge
    {
        [Required]
        public string Amount { get; set; }
        public string Naration { get; set; }
        public bool SMS { get; set; }
    }
}
