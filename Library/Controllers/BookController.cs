using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBookService _serive;
        public BookController(IBookService serive, ICatalogService catalogService)
        {
            _serive = serive;
            _catalogService = catalogService;
        }
        public IActionResult Index()
        {
            var book = _serive.GetAll();
            return View("Index",book);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            var catalog = _catalogService.GetAll();

            ViewBag.catalogs = new SelectList(catalog, "CatalogId", "CatalogName");
            return View();
        }
        [HttpPost]
        public IActionResult Store(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }
            var result = _serive.Create(book);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Create", book);
        }
        [HttpGet]
        public IActionResult Edit (int BookId) 
        {
            var catalog = _catalogService.GetAll();
            ViewBag.catalogs = new SelectList(catalog, "CatalogId", "CatalogName");

            var book = _serive.Get(BookId);
            return View("Edit",book);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            var result = _serive.Update(book);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete (int BookId)
        {
            var book = _serive.Get(BookId);
            return View("Delete",book);
        }
        [HttpPost]
        public IActionResult Destroy(int bookId)
        {
            if (!ModelState.IsValid)
            {
                return View(bookId);
            }
            var result = _serive.Delete(bookId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(bookId);
        }

    }
}
