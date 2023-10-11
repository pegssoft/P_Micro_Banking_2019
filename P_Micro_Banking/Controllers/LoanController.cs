using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.Loan;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class LoanController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataAccessLayer _DataAccessLayer;

        //Site cookie settings
        public const string SessionKeyFirstName = "_FirstName";
        public const string SessionKeyLastNmame = "_LastName";
        public const string SessionKeyCompany = "_Company";
        public const string SessionKeyBranch = "_Branch";
        public const string SessionKeyAddress = "_Address";
        public const string SessionKeyUserName = "_UserName";
        public const string SessionKeyEmail = "_Email";
        public const string SessionKeyPhoneNumber = "_Phone_Number";
        public const string SessionKeyRoleId = "_RoleId";

        public LoanController(
           IHttpContextAccessor httpContextAccessor,
           ILoggerFactory loggerFactory,
           ApplicationDbContext dbContext,
          IDataAccessLayer DataAccessLayer)
        {

            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _dbContext = dbContext;
            _DataAccessLayer = DataAccessLayer;
        }

        [HttpPost]
        public IActionResult GetCustomerNameForLoan(DisbursmentDetails disbursment)
        {
            DisbursmentDetails AccName = new DisbursmentDetails();
            AccName = _DataAccessLayer.GetCustomerNameForLoan(disbursment.CustomerID);

            if (string.IsNullOrEmpty(AccName.AccountName))
            {
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
        }

        [HttpPost]
        public IActionResult GetLoanForRepayment(RepyTransScherch rpy)
        {
            RepyTransScherch AccName = new RepyTransScherch();
            AccName = _DataAccessLayer.GetLoanForRepayment(rpy.AccountNumber);

            if (string.IsNullOrEmpty(AccName.AccountHolderName))
            {
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
        }

        [HttpPost]
        public IActionResult GetInterestRate([FromBody] State state)
        {
            UtilityClass AccName = new UtilityClass();
            AccName = _DataAccessLayer.GetLoanInterestRate(state.Sate_Name);

            return Json(AccName);
        }

        [HttpGet]
        public IActionResult LoanAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoanAccount(LoanAccount loanAccount)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            LoanAccount dis = new LoanAccount();
            dis = _DataAccessLayer.GetDisbursmentSchName(loanAccount.CustomerID);

            if (string.IsNullOrEmpty(dis.FullNames))
            {
                return Json(new { isValid = false, dep = dis });
            }
            else
            {
                return Json(new { isValid = true, dep = dis });
            }
        }

        [HttpGet]
        public IActionResult ViewAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ViewAccount(AccountTransactions account)
        {
            AccountTransactions AccName = new AccountTransactions();
            AccName = _DataAccessLayer.GetLoan(account.AccountNumber);

            if (string.IsNullOrEmpty(AccName.FullNames))
            {
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
        }

        [HttpGet]
        public IActionResult Disbursment()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Disbursment(DisbursmentDetails disbursment)
        {
            bool status = false;
            string message = string.Empty;

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {

            }
            else
            {
                string messages = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                status = false;
                message = messages;
                return Json(new { isValid = status, msg = message });
            }

            string mobile = disbursment.PhoneNumber.TrimStart(new char[] { '0' });
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            Tuple<string, string> response = _DataAccessLayer.InsertDisbursment(disbursment, username, branchcode);

            if (response.Item1 == "Posted")
            {
                string accn = response.Item2.Substring(0, 3);
                string accno = response.Item2.Substring(response.Item2.Length - 3);
                string domacc = accn + "XXX" + accno;
                bool isInternet = IsInternetAccess();
                if (isInternet == true)
                {
                    string msg = "LOAN: " + domacc + " Amt:NGN" + Convert.ToDecimal(disbursment.LoanAmount).ToString("#,##00.00") + " Date: " + DateTime.Now + " Desc: " + "Loan Disburment."  + " Your repayment should begin on "+ DateTime.Now.AddMonths(1) + " Thank you";
                    sendSMS("234" + mobile, msg);
                }
                status = true;
                message = "Disbursment Created";
            }
            else
            {
                status = false;
                message = response.Item1;
            }


            return Json(new { isValid = status, msg = message });
        }
        [HttpGet]
        public IActionResult Repayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Repayment(RepyTransScherch repyTrans)
        {
            bool status = false;
            string message = string.Empty;

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {

            }
            else
            {
                string messages = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                status = false;
                message = messages;
                return Json(new { isValid = status, msg = message });
            }

            string accn = repyTrans.AccountNumber1.Substring(0, 3);
            string accno = repyTrans.AccountNumber1.Substring(repyTrans.AccountNumber1.Length - 3);
            string domacc = string.Concat(accn, "XXX", accno);

            string mobile = repyTrans.PhoneNumber.TrimStart(new char[] { '0' });
            
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            Tuple<string, string> response = _DataAccessLayer.InsertCashRepayment(repyTrans, username, branchcode);

            if (response.Item1 == "Posted")
            {
                bool sms = repyTrans.SMS;
                if (sms == true)
                {
                    string msg = "Loan Repayment: " + domacc + " Amt:NGN" + Convert.ToDecimal(repyTrans.PrincipalInterestAmount).ToString("#,##00.00") + " Date: " + DateTime.Now + " Desc: " + repyTrans.Naration + " Bal: NGN" + Convert.ToDecimal(response.Item2).ToString("#,##00.00") + ". Thank you";
                    sendSMS("234" + mobile, msg);
                }
                status = true;
                message = "Posted Successful";
            }
            else
            {
                status = false;
                message = response.Item1;
            }


            return Json(new { isValid = status, msg = message });
        }

        public bool IsInternetAccess()
        {
            string host = "4.2.2.2";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }
        public IActionResult Reversal()
        {
            return View();
        }
        public async void sendSMS(string dst, string Messages)
        {
            string userID = "55883602";
            string Password = "valentineS09";
            string destination = dst;
            string senderId = "JOYZONE";
            string message = Messages;
            string Url = "http://developers.cloudsms.com.ng/api.php?userid=" + userID + "&password=" + Password + "&type=5&destination=" + destination + "&sender=" + senderId + "&message=" + message;

            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("x-lapo-eve-proc", middlekey);
                using (var response = await httpClient.GetAsync(Url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string getRes = await response.Content.ReadAsStringAsync();

                    }
                    else
                    {

                    }

                }
            }
        }

        public IActionResult GetLoanPackage()
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetLoanPackage();

            return Json(utilityClasses);
        }

        [HttpPost]
        public IActionResult logout()
        {
            HttpContext.Session.Remove(SessionKeyFirstName);
            HttpContext.Session.Remove(SessionKeyLastNmame);
            HttpContext.Session.Remove(SessionKeyCompany);
            HttpContext.Session.Remove(SessionKeyBranch);
            HttpContext.Session.Remove(SessionKeyAddress);
            HttpContext.Session.Remove(SessionKeyUserName);
            HttpContext.Session.Remove(SessionKeyEmail);
            HttpContext.Session.Remove(SessionKeyPhoneNumber);
            HttpContext.Session.Remove(SessionKeyRoleId);
            return RedirectToAction("Login", "Account");
        }

    }
}
