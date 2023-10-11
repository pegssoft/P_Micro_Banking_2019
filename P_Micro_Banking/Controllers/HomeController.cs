using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IDataAccessLayer _DataAccessLayer;
        private readonly SignInManager<ApplicationUser> _signInManager;

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

        public HomeController(
           IHttpContextAccessor httpContextAccessor,
           ILoggerFactory loggerFactory,
           ApplicationDbContext dbContext,
          IDataAccessLayer DataAccessLayer,
          ILogger<HomeController> logger,
          SignInManager<ApplicationUser> signInManager)
        {

            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _DataAccessLayer = DataAccessLayer;
            _logger = logger;
            _signInManager = signInManager;
        }

       

        public IActionResult Index()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
