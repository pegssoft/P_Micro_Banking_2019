using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.Report;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class ReportController : Controller
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

        public ReportController(
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
        public IActionResult DisbursmentReport()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            DisburmentTotal disburment = new DisburmentTotal();
            disburment = _DataAccessLayer.GetDisBursment(DateTime.Now, DateTime.Now);
            return View(disburment);
        }

        [HttpPost]
        public IActionResult DisbursmentReport(DateTime startDate, DateTime enddate)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            DisburmentTotal disburment = new DisburmentTotal();
            disburment = _DataAccessLayer.GetDisBursment(startDate, enddate);
            return View(disburment);
        }

        [HttpGet]
        public IActionResult NewClient()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<NewClient> client = new List<NewClient>();
            client = _DataAccessLayer.GetNewClient(DateTime.Now, DateTime.Now);
            return View(client);
        }

        [HttpPost]
        public IActionResult NewClient(DateTime startDate, DateTime enddate)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<NewClient> client = new List<NewClient>();
            client = _DataAccessLayer.GetNewClient(startDate, enddate);
            return View(client);
        }

        public IActionResult TrailBalance()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            var today = DateTime.Now;
            var StartDate = new DateTime(today.Year, today.Month, 1);

            DateTime MonthStartDate = Convert.ToDateTime(StartDate.ToString("yyy/MM/dd"));
            var yesterday = DateTime.Now;

            ReportUtility report = new ReportUtility();
            report = _DataAccessLayer.GetTrailbalance(yesterday, MonthStartDate, DateTime.Now.Date);

            return View(report);
        }

        public IActionResult LoanBalance()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            LoanBalanceTotal client = new LoanBalanceTotal();
            client = _DataAccessLayer.GetLoanBalance();
            return View(client);
        }
        [HttpGet]
        public IActionResult Transaction()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            Transction transction = new Transction();
            transction = _DataAccessLayer.GetTransaction(DateTime.Now, DateTime.Now);

            return View(transction);
        }
        [HttpPost]
        public IActionResult Transaction(DateTime startDate, DateTime enddate)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            Transction transction = new Transction();
            transction = _DataAccessLayer.GetTransaction(startDate, enddate);

            return View(transction);
        }

       
        [HttpGet]
        public IActionResult TransReportOfficer()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            TransOfficer transction = new TransOfficer();
            transction = _DataAccessLayer.GetTransOfficer("", 1, DateTime.Now, DateTime.Now);

            return View(transction);
        }

        [HttpPost]
        public IActionResult TransReportOfficer(int Selection, string officer, DateTime startDate, DateTime enddate)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            TransOfficer transction = new TransOfficer();
            transction = _DataAccessLayer.GetTransOfficer(officer, Selection, startDate, enddate);

            return View(transction);
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
