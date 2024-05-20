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
            int pageNumber = page ?? 1;
            int pageSize = 10;

            var totalCount = _service.GetAll().Count();

            int skip = (pageNumber - 1) * pageSize;

            var borrows = _service.GetAll().Skip(skip).Take(pageSize).ToList();

            ViewBag.TotalCount = totalCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View("Index", borrows);
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

            //var catalog = _catalog.GetAll();
            //ViewBag.Catalog = new SelectList(catalog, "CatalogId", "CatalogName");


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
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int BorrowId)
        {
            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");


            
            var borrow = _service.Get(BorrowId);
           
            return View("Edit",borrow);
        }

        [HttpPost]
        public IActionResult Update(Borrow borrow)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit",borrow);
            }
            var result = _service.Updade(borrow);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Edit",borrow);
        }

        [HttpGet]
        public IActionResult Delete(int BorrowId)
        {


            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");
            
            var borrow = _service.Get(BorrowId);

            return View("Delete",borrow);
        }
        [HttpPost]
        public IActionResult Destroy(int borrowId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", borrowId);
            }
            var result = _service.Delete(borrowId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", borrowId);
        }
    }
}
