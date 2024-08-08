using Dapper;
using Library.Data;

using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Newtonsoft.Json;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly DapperDbConnext _dapper;
        private readonly IAccountService _account;
        private readonly IRoleService _rolservice;
        private readonly IAppUserpermissionService _permission;
        private readonly IHttpContextAccessor _context;
        public AccountController(DapperDbConnext dapper, IAccountService account, IHttpContextAccessor context, IRoleService rolservice, IAppUserpermissionService permission)
        {
            _dapper = dapper;
            _account = account;
            _context = context;
            _rolservice = rolservice;
            _permission = permission;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        
        [HttpPost]
        public IActionResult Register(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", appUser);
            }
            var user = _account.Create(appUser);
            if(user != null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View("Register", appUser);

        }


        [HttpGet]
        public IActionResult Login()
        {

            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(AppUser appUser, string? returnUrl = null)
        {
            
            returnUrl = returnUrl ?? Url.Content("/Home");
            ViewBag.returnUrl = returnUrl;


            if (!ModelState.IsValid)
            {
                return View("Login", appUser);
            }
            var result = _account.GetUsers(appUser);
            if (result != null)
            {
                var roles = _rolservice.GetUserRol(result.UserName);
                var permissions = _permission.userpermission(result.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.UserName),
                };
                
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                // Add permissions to claims with logging
                foreach (var permission in permissions)
                {
            
                    claims.Add(new Claim("UserPermission", permission));
                }


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {

                };
                _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity), authProperties);


                // Set the permissions in the session
                var permissionString = string.Join(",", permissions);
                HttpContext.Session.SetString("UserPermission", permissionString);

                var rolestring = string.Join(",", roles);
                _context.HttpContext.Session.SetString("roles", rolestring);
                _context.HttpContext.Session.SetInt32("AppUserId", result.AppUserId);
                _context.HttpContext.Session.SetString("UserName", result.UserName);

                TempData["ToastrMessage"] = "Login SuccessFully...!";
                TempData["ToastrMessageType"] = "success";
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password ");
            }
            return View("Login", appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> LogOut()
        {
         
           await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

       
    }
}
