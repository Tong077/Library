using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _service;
        private readonly ICustomerService _cus;
        private readonly ILibrarianService _lia;
        private readonly IBorrowDetailService _borrowdetail;
        private readonly IBookService _book;
        private readonly ICatalogService _catalog;
        public BorrowController(IBorrowService service, ICustomerService cus, ILibrarianService lia, IBorrowDetailService borrowdetail, IBookService book, ICatalogService catalog)
        {
            _service = service;
            _cus = cus;
            _lia = lia;
            _borrowdetail = borrowdetail;
            _book = book;
            _catalog = catalog;
        }

        public IActionResult Index(int? page)
        {
           
            var borrow = _service.GetAll();
            return View("Index", borrow);
        }

       

        [HttpGet]
        public IActionResult Create()
        {
            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");

            var book = _book.GetAll();
            ViewBag.Book = new SelectList(book, "BookId", "Catalog.CatalogName");
            //-------------------------------------------------------------------------//
           


            return View("Create");
        }
        [HttpPost]
        public IActionResult Store(Borrow borrow, BorrowDetail borrowDetail)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            var result = _service.Create(borrow, borrowDetail);
            if (result)
            {
               return RedirectToAction("Index", "BorrowDetial");
            }
            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int BorrowId, int BorrowDetailId)
        {
            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");

            //-------------------------------------------------------------------------//
            var book = _book.GetAll();
            ViewBag.Book = new SelectList(book, "BookId", "Catalog.CatalogName");


            var borrow = _service.Get(BorrowId, BorrowDetailId);
            if(borrow == null)
            {
                return NotFound();
            }
           
            return View("Edit", borrow);
        }

        [HttpPost]
        public IActionResult Update(Borrow borrow, BorrowDetail borrowDetail)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            var result = _service.Updade(borrow, borrowDetail);
            if (result)
            {
                return RedirectToAction("Index", "BorrowDetial");
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int BorrowId, int BorrowDetailId)
        {

            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");
            //-------------------------------------------------------------------------//

            var book = _book.GetAll();
            ViewBag.Book = new SelectList(book, "BookId", "Catalog.CatalogName");

            var borrow = _service.Get(BorrowId, BorrowDetailId);
            if (borrow == null)
            {
                return NotFound();
            }

            return View("Delete",borrow);
        }

        [HttpPost]
        public IActionResult Destroy(int BorrowId, int BorrowDetailId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete");
            }
            var result = _service.Delete(BorrowId, BorrowDetailId);
            if (result != null)
            {
                return RedirectToAction("Index", "BorrowDetial");
            }
            return View("Delete");
        }
    }
}
