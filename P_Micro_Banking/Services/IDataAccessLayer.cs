using P_Micro_Banking.Models;
using P_Micro_Banking.Models.Customer;
using P_Micro_Banking.Models.Deposite;
using P_Micro_Banking.Models.Loan;
using P_Micro_Banking.Models.Menu;
using P_Micro_Banking.Models.Report;
using P_Micro_Banking.Models.TrnsPosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Services
{
    public interface IDataAccessLayer
    {
        Task<List<Menu>> Menu(int code);
        Task<List<MenuChild>> MenuChild(int code, string roleid);
        string GetRoleIdAsync(string Userid);
        IEnumerable<UtilityClass> GetBranches();
        IEnumerable<UtilityClass> GetRoles();
        IEnumerable<UtilityClass> GetState();
        IEnumerable<UtilityClass> GetCity(string state);
        IEnumerable<UtilityClass> GetRelationshipOfficer(string BranchCode);
        IEnumerable<UtilityClass> GetDepositePackage(string PackageClass);
        UtilityClass GetLoanInterestRate(string PackageCode);
        IEnumerable<UtilityClass> GetLoanPackage();
        UtilityUserClass GetDepositePackageItem(string PackageID);
        string InsertUserRole(UserRole userRole);
        string InsertCustomer(CustomerOnboarding onboarding, string user, string branchcode);

        IEnumerable<CustomerV> GetCustomer(string branchcode);

        CustomerOnboarding GetCustomerSingle(string CustomerID);

        NewAccount GetCustomerSingleName(string CustomerID);

        DepositeAcc GetCustomerNameAccount(string CustomerID);

        string InsertNewAccount(NewAccount account, string user, string branchcode);

        string InsertFixedDeposite(NewAccounts account, string user, string branchcode);

        AccountTransaction GetCustomerAccNBTD(string Account);

        TransactionSearch GetTransactionSearch(string Account);

        TransactionSearch GetTransactionSearchForFixed(string Account);

        Tuple<string, string> InsertOpenning(TransactionSearch account, string user, string branchcode);

        Tuple<string, string> InsertCashDeposit(TransactionSearch account, string user, string branchcode);

        string InsertPostFixedDepositeInterest(TransactionSearch account, string user, string branchcode);

        Tuple<string, string> InsertCashWithdrawer(TransactionSearch account, string user, string branchcode);

        DisbursmentDetails GetCustomerNameForLoan(string CustomerID);

        Tuple<string, string> InsertDisbursment(DisbursmentDetails disbursment, string user, string branchcode);

        LoanAccount GetDisbursmentSchName(string CustomerID);

        AccountTransactions GetLoan(string account);

        RepyTransScherch GetLoanForRepayment(string account);

        Tuple<string, string> InsertCashRepayment(RepyTransScherch account, string user, string branchcode);

        IEnumerable<GlOperations> GetDrawerBalance(string user, string status);

        IEnumerable<UtilityClass> GetTeller(string user, string status);
        IEnumerable<UtilityClass> GetToAccount(string user, string status);

        string InsertGLCredit(GLTransPosting account, string user, string branchcode);

        string InsertGLDebit(GLTransPosting account, string user, string branchcode);

        ReportUtility GetTrailbalance(DateTime startdate, DateTime mStartdate, DateTime mEnddate);

        IEnumerable<NewClient> GetNewClient(DateTime mStartdate, DateTime mEnddate);

        LoanBalanceTotal GetLoanBalance();

        DisburmentTotal GetDisBursment(DateTime mStartdate, DateTime mEnddate);

        Transction GetTransaction(DateTime mStartdate, DateTime mEnddate);

        TransOfficer GetTransOfficer(string Officer, int selection, DateTime mStartdate, DateTime mEnddate);

        string smsCharge(smsCharge charge, string user, string branchcode);
    }
}
