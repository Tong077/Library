using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBookService _serive;
        private readonly IBorrowDetailService _detail;
        public BookController(IBookService serive, ICatalogService catalogService, IBorrowDetailService detail)
        {
            _serive = serive;
            _catalogService = catalogService;
            _detail = detail;
        }

        
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1; // If no page number is provided, default to page 1
            int pageSize = 10; // Number of books per page

            // Get the total count of books
            var totalCount = _serive.GetAll().Count();

            // Calculate the number of items to skip
            int skip = (pageNumber - 1) * pageSize;

            // Get the paginated list of books
            var books = _serive.GetAll().Skip(skip).Take(pageSize).ToList();

            var borrowDetails = _detail.GetAll();

            // Use SelectMany to flatten the list of BookIds
            var selectedBookCodes = borrowDetails.SelectMany(bd => new[] { bd.BookId }).ToList();

            // Pass the paginated list of books and total count to the view

            ViewBag.TotalCount = totalCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.SelectedBookCodes = selectedBookCodes;


            return View(books);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            var catalog = _catalogService.GetAll();
            ViewBag.catalogs = new SelectList(catalog, "CatalogId", "CatalogName");
            return View("Create");
        }
      

        [HttpPost]
        public IActionResult Store(Book book)
        {
            if (ModelState.IsValid) {
              bool bookcodeexist =  _serive.BookCodeExists(book.BookCode);
                if (bookcodeexist) {
                    ModelState.AddModelError(string.Empty, "The BookCode already exists.");
                    return View("Create");
                }
                bool success = _serive.Create(book);
                if (success) 
                { 
                        return RedirectToAction("Index");
                    
                }
            }
            return View("Create",book);

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
