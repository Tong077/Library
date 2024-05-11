using Dapper;
using Library.Areas.Account.Data;
using Library.Areas.Account.Models;
using Library.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        private readonly DapperdbContext _dapper;

        public AccountController(DapperdbContext dapper)
        {
            _dapper = dapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", appUser);
            }
            var query = "SELECT * FROM AppUser WHERE UserName = @UserName AND Password = @Password";
            var result = _dapper.Connection.QueryFirstOrDefault<AppUser>(query, appUser);

            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
            return View("Login", appUser);



        }
    }
}
