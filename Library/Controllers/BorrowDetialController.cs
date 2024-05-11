using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class BorrowDetialController : Controller
    {
        private readonly ICustomerService _cus;
        private readonly ICatalogService _ca;
        private readonly IBorrowService _borrow;
        private readonly IBookService _book;
        private readonly IBorrowDetailService _service;
        public BorrowDetialController(IBorrowDetailService service, IBorrowService borrow, IBookService book, ICatalogService ca, ICustomerService cus)
        {
            _service = service;
            _borrow = borrow;
            _book = book;
            _ca = ca;
            _cus = cus;
        }
        public IActionResult Index()
        {
            var borrowdetail = _service.GetAll();
            return View("Index",borrowdetail);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //var customer = _borrow.GetAll();
            //ViewBag.Borrow = new SelectList(customer, "BorrowId", "BorrowCode");
            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");

            //-------------------------------------------------------------


            //var book = _book.GetAll();

            //ViewBag.Book = new SelectList(book, "BookId", "CatalogId", "CatalogName");
            var catalog = _ca.GetAll();
            ViewBag.Catalog = new SelectList(catalog,"CatalogId", "CatalogName");
            return View("Create");
        }

        [HttpPost]
        public IActionResult Store(BorrowDetail borrowdetail)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", borrowdetail);
            }
            var result = _service.Create(borrowdetail);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Create", borrowdetail);
        }

        [HttpGet]
        public IActionResult Edit(int BorrowDetailId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(BorrowDetail borrowdetail)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int BorrowDetailId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destroy(int borrowdetailId)
        {
            return View();
        }
    }
}
