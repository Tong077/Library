using Dapper;
using Library.Data;
using Library.Migrations;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly DapperDbConnext _dapper;

        public AccountController(DapperDbConnext dapper)
        {
            _dapper = dapper;
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
                return View("Register",appUser);
            }
            var insert = "INSERT INTO AppUser(UserName,Password) VALUES(@UserName,@Password);";
            var result = _dapper.Connection.Execute(insert, new
            {
                UserName = appUser.UserName,
                Password = appUser.Password
            });
            if (result != null)
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

            var query = "SELECT * FROM AppUser WHERE UserName=@UserName AND Password=@Password;";
            var result = _dapper.Connection.QueryFirstOrDefault<AppUser>(query, new
            {
                appUser.Password,
                appUser.UserName
            });

            if (result != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password ");
            }
            return View("Login", appUser);

        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View("LogOut");
        }

        [HttpPost]
        public IActionResult LogOut(AppUser appUser)
        {
            if(appUser == null)
            {
                return View("LogOut", appUser);
            }
            var sql = "UPDATE APPUSER SET IsHiDden=@IsHiDden,Username=@Username,Password=@Password Where AppUserId=@AppUserId";
            var logout = _dapper.Connection.Execute(sql, new { @AppUserId = appUser.AppUserId });
            if(logout != null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View("LogOut", appUser);
        }

    }
}
