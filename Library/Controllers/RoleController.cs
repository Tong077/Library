using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _service;
        private readonly IAppUserService _user;
        private readonly IHttpContextAccessor _context;
        public RoleController(IRoleService service, IAppUserService user, IHttpContextAccessor context)
        {
            _service = service;
            _user = user;
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _service.GetAllRoles();
            return View("Index",roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = _user.GetAll();

            ViewBag.User = new SelectList(user, "AppUserId", "UserName");
            return View();
        }
        [HttpPost]
        public IActionResult Store(Role role)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", role);
            }
            var result = _service.CreateRole(role);
            if(result)
            {
                return RedirectToAction("Index");
            }
            return View("Create", role);
        }

        [HttpGet]
        public IActionResult Edit(int RoleId)
        {
            var user = _user.GetAll();
            ViewBag.User = new SelectList(user, "AppUserId", "UserName");

            var role = _service.GetRole(RoleId);
            return View("Edit", role);
        }
        [HttpPost]
        public IActionResult Update(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", role);
                
            }
            var result = _service.UpdateRole(role);
            if(result)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", role);
        }

        [HttpGet]
        public IActionResult Delete(int RoleId)
        {
            var user = _user.GetAll();
            ViewBag.User = new SelectList(user, "AppUserId", "UserName");

            var role = _service.GetRole(RoleId);
            return View("Delete", role);
        }
        [HttpPost]
        public IActionResult Destroy(int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", RoleId);
            }
            var result = _service.DeleteRole(RoleId);
            if(result)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", RoleId);
        }

        

    }
}
