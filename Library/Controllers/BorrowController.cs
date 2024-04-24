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
        public BorrowController(IBorrowService service,ICustomerService cus,ILibrarianService lia)
        {
            _service = service;
            _cus = cus;
            _lia = lia;
        }

		public IActionResult Index()
        {
            var borrows = _service.GetAll();
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


            return View("Create");
        }
        [HttpPost]
        public IActionResult Store(Borrow borrow)
        {
            if(!ModelState.IsValid)
            {
                return View("Create",borrow);
            }
            var result = _service.Create(borrow);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Create",borrow);
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
