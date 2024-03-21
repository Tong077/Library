using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class AppUserpermissionController : Controller
    {
        private readonly IAppUserpermissionService _service;
        public AppUserpermissionController(IAppUserpermissionService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(AppuserPermission appuserPermission)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit (int appuserPermissionId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update (AppuserPermission appuserPermission)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int appuserPermissionId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Destroy(int appuserPermissionId)
        {
            return View();
        }
    }
}
