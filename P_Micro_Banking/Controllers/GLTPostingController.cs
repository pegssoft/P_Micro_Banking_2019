using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.TrnsPosting;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class GLTPostingController : Controller
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

        public GLTPostingController(
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

        public IActionResult Drawer()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }

            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);

            IEnumerable<GlOperations> glOperations = new List<GlOperations>();
            glOperations = _DataAccessLayer.GetDrawerBalance(username, "Active");

            return View(glOperations);
        }
        [HttpGet]
        public IActionResult GLCreditPosting()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult GLCreditPosting(GLTransPosting gLTrans)
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

            string response = _DataAccessLayer.InsertGLCredit(gLTrans, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Posted Successful";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
        }
        [HttpGet]
        public IActionResult GLDebitPosting()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult GLDebitPosting(GLTransPosting gLTrans)
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

            string response = _DataAccessLayer.InsertGLDebit(gLTrans, username, branchcode);

            if (response == "Posted")
            {
                status = true;
                message = "Posted Successful";
            }
            else
            {
                status = false;
                message = response;
            }


            return Json(new { isValid = status, msg = message });
        }

        public IActionResult GetTeller()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetTeller(username, "Active");

            return Json(utilityClasses);
        }

        public IActionResult GetToAccount()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var username = httpContext.Session.GetString(SessionKeyUserName);
            var branchcode = httpContext.Session.GetString(SessionKeyBranch);
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetToAccount(username, "Active");

            return Json(utilityClasses);
        }

    }
}
