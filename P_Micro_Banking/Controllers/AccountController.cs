using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P_Micro_Banking.Models;
using P_Micro_Banking.Models.AccountViewModel;
using P_Micro_Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
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


        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
          IHttpContextAccessor httpContextAccessor,
           ILoggerFactory loggerFactory,
           ApplicationDbContext dbContext,
          IDataAccessLayer DataAccessLayer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _dbContext = dbContext;
            _DataAccessLayer = DataAccessLayer;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            bool status = true;
            string message = string.Empty;
            string messageCode = string.Empty;

            if (ModelState.IsValid)
            {

            }
            else
            {
                message = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                messageCode = "MDSERR_000";
                status = false;
                return Json(new { isValid = status, Message = message, MessageCode = messageCode });
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            //if okay, get cookie data
            if (result.Succeeded)
            {
                //get the login user
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                var roles = _DataAccessLayer.GetRoleIdAsync(user.Id);

                _logger.LogInformation(1, "User logged in.");

                HttpContext httpContext = _httpContextAccessor.HttpContext;

                if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
                {
                    //Set cookie/Session data
                    HttpContext.Session.SetString(SessionKeyFirstName, user.First_Name);
                    HttpContext.Session.SetString(SessionKeyLastNmame, user.First_Last);
                    HttpContext.Session.SetString(SessionKeyCompany, user.Company);
                    HttpContext.Session.SetString(SessionKeyBranch, user.Branch);
                    HttpContext.Session.SetString(SessionKeyAddress, user.Address);
                    HttpContext.Session.SetString(SessionKeyUserName, user.UserName);
                    HttpContext.Session.SetString(SessionKeyEmail, user.Email);
                    HttpContext.Session.SetString(SessionKeyPhoneNumber, user.PhoneNumber);
                    HttpContext.Session.SetString(SessionKeyRoleId, roles);

                }

                status = true;
                message = "Login Successful";
                messageCode = "LGNSCF_101";
            }
            else
            {
                status = false;
                message = "Login Failed, Please check your credentials and try again";
                messageCode = "LGNFLD_001";
            }

            return Json(new { isValid = status, Message = message, MessageCode = messageCode });
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            bool status = true;
            string message = string.Empty;
            string messageCode = string.Empty;

            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email == null)
            {

            }
            else
            {
                status = false;
                message = "User already exist";
                messageCode = "RGTFld_002";

                return Json(new { isValid = status, Message = message, MessageCode = messageCode });
            }

            if (ModelState.IsValid)
            {

            }
            else
            {
                message = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                messageCode = "MDSERR_000";
                status = false;
                return Json(new { isValid = status, Message = message, MessageCode = messageCode });
            }

            //map the user object
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                First_Name = model.First_Name,
                First_Last = model.First_Last,
                Company = model.Company,
                Branch = model.Branch,
                Address = model.Address,
                Phone_Number = model.Phone_Number
                                           ,
                PhoneNumber = model.Phone_Number

            };

            //Create the user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var Createduser = await _userManager.FindByNameAsync(model.Email);
                // add newly created user to role
                //await _userManager.AddToRoleAsync(user, Convert.ToString(registerViewModel.uRole));
                UserRole userRole = new UserRole()
                {
                    UserId = Createduser.Id,
                    RoleId = Convert.ToString(model.Role)
                };

                var RtnVlu = _DataAccessLayer.InsertUserRole(userRole);

                status = true;
                message = "Register Successful";
                messageCode = "RGTSUF_102";
            }
            else
            {
                status = false;
                message = result.Errors.Select(x => x.Description).FirstOrDefault();
                messageCode = "RGTFld_002";
            }

            return Json(new { isValid = status, Message = message, MessageCode = messageCode });
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (string.IsNullOrEmpty(httpContext.Session.GetString(SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            bool status = true;
            string message = string.Empty;
            string messageCode = string.Empty;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    // return RedirectToAction(nameof(Login), new { Message = ManageMessageId.ChangePasswordSuccess });
                    status = true;
                    message = "Password Change Successfully";
                    messageCode = "";
                }
                else
                {
                    //AddErrors(result);
                    //return View(model);
                    status = false;
                    message = "Password Change Faild";
                    messageCode = "";
                }
            }
            // return RedirectToAction(nameof(Login), new { Message = ManageMessageId.Error });
            return Json(new { Status = status, Message = message, MessageCode = messageCode });
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            return _userManager.FindByNameAsync(httpContext.Session.GetString(SessionKeyUserName));
        }

        public IActionResult GetBranches()
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetBranches();

            return Json(utilityClasses);

        }
        public IActionResult GetURoles()
        {
            IEnumerable<UtilityClass> utilityClasses = new List<UtilityClass>();

            utilityClasses = _DataAccessLayer.GetRoles();

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
            return RedirectToAction(nameof(Login));
        }
    }
}
