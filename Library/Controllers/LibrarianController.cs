using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LibrarianController : Controller
    {
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
        public IActionResult Delete(Librarian librarian)
        {
            return View();
        }
    }
}
