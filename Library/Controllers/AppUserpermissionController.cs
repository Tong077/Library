using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class AppUserpermissionController : Controller
    {
        private readonly IAppUserpermissionService _service;
        private readonly IAppUserService _user;

        public AppUserpermissionController(IAppUserService user, IAppUserpermissionService service)
        {
            _user = user;
            _service = service;
        }


        public IActionResult Index()
        {
            var result = _service.GetAll();

            return View("Index", result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = _user.GetAll();

            ViewBag.User = new SelectList(user, "AppUserId", "UserName");
            
            return View("Create");
        }

        [HttpPost]
        public IActionResult Store(AppuserPermission appuserPermission)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", appuserPermission);
            }
            var result = _service.Create(appuserPermission);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Create", appuserPermission);
        }

        [HttpGet]
        public IActionResult Edit (int appuserPermissionId)
        {
            var user = _user.GetAll();

            ViewBag.User = new SelectList(user, "AppUserId", "UserName");

            var get = _service.GetById(appuserPermissionId);

            return View("Edit",get);
        }

        [HttpPost]
        public IActionResult Update (AppuserPermission appuserPermission)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit",appuserPermission);
            }
            var result = _service.Update(appuserPermission);
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return View("Edit",appuserPermission);
        }

        [HttpGet]
        public IActionResult Delete(int appuserPermissionId)
        {
            var user = _user.GetAll();

            ViewBag.User = new SelectList(user, "AppUserId", "UserName");

            var get = _service.GetById(appuserPermissionId);
            return View("Delete",get);
        }

        [HttpPost]
        public IActionResult Destroy(int appuserPermissionId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", appuserPermissionId);
            }
            var result = _service.Delete(appuserPermissionId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", appuserPermissionId);
        }
    }
}
