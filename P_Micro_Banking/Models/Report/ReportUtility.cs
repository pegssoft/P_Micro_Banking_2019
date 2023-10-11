using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.Report
{
    public class ReportUtility
    {
        public List<Income> Incomes { get; set; }
        public List<Liability> Liabilitys { get; set; }
    }

    public class Income
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Opening { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Closing { get; set; }
    }

    public class Liability
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Opening { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Closing { get; set; }
    }

    public class NewClient
    {
        public string count { get; set; }
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public string RelationshipOfficerID { get; set; }

    }

    public class DisburmentTotal
    {
        public string PrincipalAmount { get; set; }
        public string PrincipalInterestAmount { get; set; }
        public string TotalInterest { get; set; }
        public List<Disburment> disburments { get; set; }
    }

    public class Disburment
    {
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string LaonName { get; set; }
        public string Tenor { get; set; }
        public string Date { get; set; }
        public string PrincipalAmount { get; set; }
        public string PrincipalInterestAmount { get; set; }
        public string TotalInterest { get; set; }
        public string RelationshipOfficerID { get; set; }
    }

    public class Transction
    {
        public string RepaymentTotal { get; set; }
        public string DepositeTotal { get; set; }
        public string WidthdrawerTotal { get; set; }
        public List<Repayment> repayments { get; set; }
        public List<Deposite> deposite { get; set; }
        public List<Widthdrawer> widthdrawer { get; set; }
    }

    public class Repayment
    {
        public string AccountNumber { get; set; }
        public string ReletionshipId { get; set; }
        public string Amount { get; set; }
    }

    public class Deposite
    {
        public string AccountNumber { get; set; }
        public string ReletionshipId { get; set; }
        public string Amount { get; set; }
    }

    public class Widthdrawer
    {
        public string AccountNumber { get; set; }
        public string ReletionshipId { get; set; }
        public string Amount { get; set; }
    }

    public class LoanBalance
    {
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountName { get; set; }
        public string ReletionshipId { get; set; }
        public string Paidby { get; set; }
        public string Count { get; set; }
        public string Totalpaid { get; set; }
        public string Balance { get; set; }
    }

    public class LoanBalanceTotal
    {
        public string Totalpaid { get; set; }
        public string Balance { get; set; }
        public List<LoanBalance> loans { get; set; }
    }

    public class TransOfficer
    {
        public string TotalSavingsBal { get; set; }
        public string TotalLoanBal { get; set; }
        public List<Depsit> deposite { get; set; }
        public List<LoanBal> loans { get; set; }
    }

    public class Depsit
    {
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountName { get; set; }
        public string Balance { get; set; }
    }

    public class LoanBal
    {
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountName { get; set; }
        public string Balance { get; set; }
    }
}




