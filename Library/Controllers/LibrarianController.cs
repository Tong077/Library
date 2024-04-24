using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService _service;
        public LibrarianController(ILibrarianService service)
        {
            _service = service;
        }
		public IActionResult Index()
        {
            var librarian = _service.GetAll();
            return View("Index", librarian);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Store(Librarian librarian)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",librarian);
            }
            var result = _service.Create(librarian);
            if (result)
            {
                return RedirectToAction("Index");
            }
             return View("Create",librarian);
        }

        [HttpGet]
        public  IActionResult Edit(int LibrarianId)
        { 
            var li = _service.Get(LibrarianId);
            return View("Edit", li);
        }

        [HttpPost]
        public IActionResult Update(Librarian librarian)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", librarian); 
            }
            var result = _service.Update(librarian);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", librarian);
        }

        [HttpGet]
        public IActionResult Delete(int LibrarianId)
        {
            var li = _service.Get(LibrarianId);
            return View("Delete", li);
        }
        [HttpPost]
        public IActionResult Destroy(int LibrarianId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", LibrarianId);
            }
            var result = _service.Delete(LibrarianId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Delete",LibrarianId);
        }
    }
}
