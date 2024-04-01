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
        public IActionResult Create(Librarian librarian)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int LibrarianId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Librarian librarian)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int LibrarainId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destroy(int LibrarianId)
        {
            return View();
        }
    }
}
