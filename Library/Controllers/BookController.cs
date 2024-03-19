using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (Book book)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit (int BookId) 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete (int BookId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destroy(int bookId)
        {
            return View();
        }

    }
}
