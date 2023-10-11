using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.Deposite;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class DepositeController : Controller
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

        public DepositeController(
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

        [HttpGet]
        public IActionResult DepositeAccount()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult DepositeAccount(DepositeAcc depositeAcc)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            DepositeAcc deposite = new DepositeAcc();
            deposite = _DataAccessLayer.GetCustomerNameAccount(depositeAcc.CustomerID);

            if (string.IsNullOrEmpty(deposite.FullNames))
            {
                return Json(new { isValid = false, dep = deposite });
            }
            else
            {
                return Json(new { isValid = true, dep = deposite });
            }
            //return Json(deposite);
        }

        [HttpPost]
        public IActionResult SingleCustomerName(NewAccount name)
        {
            NewAccount AccName = new NewAccount();
            AccName = _DataAccessLayer.GetCustomerSingleName(name.CustomerID);

            if (string.IsNullOrEmpty(AccName.AccountName))
            {
                //string ss = User.Identity.Name;
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
            //return Json(deposite);
        }

        [HttpPost]
        public IActionResult SingleAccTransName(TransactionSearch name)
        {
            TransactionSearch AccName = new TransactionSearch();
            AccName = _DataAccessLayer.GetTransactionSearch(name.AccountNumber);

            if (string.IsNullOrEmpty(AccName.AccountName))
            {
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
            //return Json(deposite);
        }

        [HttpPost]
        public IActionResult FixedAccTransName(TransactionSearch name)
        {
            TransactionSearch AccName = new TransactionSearch();
            AccName = _DataAccessLayer.GetTransactionSearchForFixed(name.AccountNumber);

            if (string.IsNullOrEmpty(AccName.AccountName))
            {
                return Json(new { isValid = false, dep = AccName });
            }
            else
            {
                return Json(new { isValid = true, dep = AccName });
            }
            //return Json(deposite);
        }

        [HttpGet]
        public IActionResult AddAccount()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(NewAccount newAccount)
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

            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            
            string response = _DataAccessLayer.InsertNewAccount(newAccount, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Account Created";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
        }

        [HttpGet]
        public IActionResult ViewAccount()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult ViewAccount(AccountTransaction transaction)
        {

            AccountTransaction AccName = new AccountTransaction();
            AccName = _DataAccessLayer.GetCustomerAccNBTD(transaction.AccountNumber);

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
        public IActionResult CashDeposite()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CashDeposite(TransactionSearch transaction)
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
            string accn = transaction.AccountNumber1.Substring(0, 3);
            string accno = transaction.AccountNumber1.Substring(transaction.AccountNumber1.Length - 3);
            string domacc = accn + "XXX" + accno;

            string mobile = transaction.PhoneNumber.TrimStart(new char[] { '0' });
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            Tuple<string, string> response = _DataAccessLayer.InsertCashDeposit(transaction, username, branchcode);

            if (response.Item1 == "Posted")
            {
                bool sms = transaction.SMS;
                if (sms == true)
                {
                    string msg = "Credit:" + domacc + " Amt:NGN" + Convert.ToDecimal(transaction.Amount).ToString("#,##00.00") + " Date:" + DateTime.Now + " Desc:" + transaction.Naration + " Bal:NGN" + Convert.ToDecimal(response.Item2).ToString("#,##00.00") + ". Thank you";
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




        [HttpGet]
        public IActionResult CashWithdrawer()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CashWithdrawer(TransactionSearch transaction)
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

            string accn = transaction.AccountNumber1.Substring(0, 3);
            string accno = transaction.AccountNumber1.Substring(transaction.AccountNumber1.Length - 3);
            string domacc = accn + "XXX" + accno;

            string mobile = transaction.PhoneNumber.TrimStart(new char[] { '0' });
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            Tuple<string, string> response = _DataAccessLayer.InsertCashWithdrawer(transaction, username, branchcode);

            if (response.Item1 == "Posted")
            {
                bool sms = transaction.SMS;
                if (sms == true)
                {
                    string msg = "Debit:" + domacc + " Amt:NGN" + Convert.ToDecimal(transaction.Amount).ToString("#,##00.00") + " Date:" + DateTime.Now + " Desc:" + transaction.Naration + " Bal:NGN" + Convert.ToDecimal(response.Item2).ToString("#,##00.00") + ". Thank you";
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

        [HttpGet]
        public IActionResult OpeningDeposite()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult OpeningDeposite(TransactionSearch transaction)
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
            string accn = transaction.AccountNumber1.Substring(0, 3);
            string accno = transaction.AccountNumber1.Substring(transaction.AccountNumber1.Length - 3);
            string domacc = accn + "XXX" + accno;

            string mobile = transaction.PhoneNumber.TrimStart(new char[] { '0' });
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            Tuple<string, string> response = _DataAccessLayer.InsertOpenning(transaction, username, branchcode);

            if (response.Item1 == "Posted")
            {
                bool sms = transaction.SMS;
                if (sms == true)
                {
                    string msg = "Credit:" + domacc + " Amt:NGN" + Convert.ToDecimal(transaction.Amount).ToString("#,##00.00") + " Date:" + DateTime.Now + " Desc:" + transaction.Naration + " Bal:NGN" + Convert.ToDecimal(response.Item2).ToString("#,##00.00") + ". Thank you";
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

        [HttpGet]
        public IActionResult FixedDeposite()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            

            return View();
        }

        [HttpPost]
        public IActionResult FixedDeposite(NewAccounts newAccount)
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

            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            string response = _DataAccessLayer.InsertFixedDeposite(newAccount, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Account Created";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
        }

        [HttpGet]
        public IActionResult PostFxInterest()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult PostFxInterest(TransactionSearch transaction)
        {
            bool status = false;
            string message = string.Empty;

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            string response = _DataAccessLayer.InsertPostFixedDepositeInterest(transaction, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Interest Posted Successful";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
        }

        public IActionResult MaturityDate([FromBody] MaRequest maRequest)
        {
            DateTime MatureDate = DateTime.Parse(maRequest.Startdate);
            int Months = Convert.ToInt32(maRequest.NumberMonth);
            DateTime maturityDate = MatureDate.AddMonths(Months);
            return Json(new { isValid = true, maturity = maturityDate.Date.ToShortDateString() });
        }
        [HttpGet]
        public IActionResult SMSCharge()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult SMSCharge(smsCharge charge)
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
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            string response = _DataAccessLayer.smsCharge(charge, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Sms Charge Posted Successful";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
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
