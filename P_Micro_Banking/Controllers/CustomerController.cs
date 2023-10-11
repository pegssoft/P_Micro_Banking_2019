using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.Customer;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class CustomerController : Controller
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

        public CustomerController(
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
        public IActionResult Onboarding()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Onboarding(CustomerOnboarding customer)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            bool status = false;
            string Resmessage = string.Empty;

            if (ModelState.IsValid)
            {

            }
            else
            {
                string messages = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                status = false;
                Resmessage = messages;
                return Json(new { isValid = status, message = Resmessage });
            }

           
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            string returnval = _DataAccessLayer.InsertCustomer(customer, username, branchcode);
           if(returnval == "Posted")
            {
                status = true;
                Resmessage = returnval;
            }
            else
            {
                status = false;
                Resmessage = returnval;
            }
            
            return Json(new { isValid = status, message = Resmessage });
        }

        public IActionResult CustomerView()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            IEnumerable<CustomerV> customers = new List<CustomerV>();
            customers = _DataAccessLayer.GetCustomer(branchcode);

            return View(customers);
        }

        [HttpGet]
        public IActionResult CustomerSingleView(string id)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            CustomerOnboarding customers = new CustomerOnboarding();
            customers = _DataAccessLayer.GetCustomerSingle(id);

            return View(customers);
        }


        public IActionResult GetState()
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetState();

            return Json(utilityClasses);
        }
        public IActionResult GetCity([FromBody] State state)
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetCity(state.Sate_Name);

            return Json(utilityClasses);
        }

        public IActionResult GetRelationshipOfficer()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetRelationshipOfficer(httpContext.Session.GetString(SessionKeyBranch));

            return Json(utilityClasses);
        }

        public IActionResult GetDepositePackage([FromBody] State state)
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetDepositePackage(state.Sate_Name);

            return Json(utilityClasses);
        }


        public IActionResult GetDepositePackageItem([FromBody] State state)
        {
            UtilityUserClass utilityClasses = new UtilityUserClass();

            utilityClasses = _DataAccessLayer.GetDepositePackageItem(state.Sate_Name);

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
            return RedirectToAction("Login","Account");
        }
    }
}
