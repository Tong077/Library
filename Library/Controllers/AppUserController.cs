using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	public class AppUserController : Controller
	{
		private readonly IAppUserService _service;

		public AppUserController(IAppUserService service)
		{
			_service = service;

		}

		[Authorize(Roles = "Admin")]
		public IActionResult Index()
        {
            var appuser = _service.GetAll();

            return View("Index", appuser);
        }

        [HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Store(AppUser appUser)
		{
			if (!ModelState.IsValid)
			{
				return View(appUser);
			}
			var result = _service.Create(appUser);
			if (result)
			{
				TempData["ToastrMessage"] = "User Has ben Added SuccessFully";
                TempData["ToastrMessageType"] = "success";
                return RedirectToAction("Index");
			}
            TempData["ToastrMessage"] = "Can't Create User";
            TempData["ToastrMessageType"] = "error";
            return View(appUser);
		}
		[HttpGet]
		public IActionResult Edit(int AppUserId)
		{
			var result = _service.Get(AppUserId);
			return View("Edit", result);
		}
		[HttpPost]
		public IActionResult Update(AppUser appUser)
		{
			if (!ModelState.IsValid)
			{
				return View(appUser);
			}
			var result = _service.Update(appUser);
			if (result)
			{
                TempData["ToastrMessage"] = "User Has ben Update SuccessFully";
                TempData["ToastrMessageType"] = "success";
                return RedirectToAction("Index");
			}
			return View(appUser);
		}

		[HttpGet]
		public IActionResult Delete(int AppUserId)
		{
			var result = _service.Get(AppUserId);
			return View("Delete", result);
		}
		[HttpPost]
		public IActionResult Destroy(int appUserId)
		{
			if (!ModelState.IsValid)
			{
				return View(appUserId);
			}
			var result = _service.Delete(appUserId);
			if (result)
			{
                TempData["SweetAlertMessage"] = "User deleted successfully.";
                TempData["SweetAlertType"] = "success";
                return RedirectToAction("Index");
			}
			return View(appUserId);
		}
	}
}