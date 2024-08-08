using Dapper;
using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;


namespace Library.Controllers
{
    public class BorrowDetialController : Controller
    {
        private readonly ICustomerService _cus;
        private readonly ICatalogService _ca;
        private readonly IBorrowService _borrow;
        private readonly IBookService _book;
        private readonly ILibrarianService _lia;
        private readonly IBorrowDetailService _service;
        private readonly DapperDbConnext _dapper;
        public BorrowDetialController(IBorrowDetailService service, IBorrowService borrow, IBookService book, ICatalogService ca, ICustomerService cus, ILibrarianService lia, DapperDbConnext dapper)
        {
            _service = service;
            _borrow = borrow;
            _book = book;
            _ca = ca;
            _cus = cus;
            _lia = lia;
            _dapper = dapper;
        }




        public IActionResult Index(int? page, string searchTerm)
        {
            var borrowdetails = _service.GetAll(searchTerm);

            var model = borrowdetails.Select(bd => new BorrowDetailView
            {
                BorrowDetailId = bd.BorrowDetailId,
                BorrowId = (int)bd.BorrowId,
                BookId = bd.BookId,
                Note = bd.Note,
                IsReturn = bd.IsReturn,
                ReturnDate = bd.ReturnDate,
                BookCode = bd.Book.BookCode,

                IsHidden = bd.Borrow.IsHidden,
                BorrowDate = bd.Borrow.BorrowDate,
                BorrowCode = bd.Borrow.BorrowCode,
                Depositamount = bd.Borrow.Depositamount,
                Duedate = bd.Borrow.Duedate,
                FineAmount = bd.Borrow.FineAmount,
                Memo = bd.Borrow.Memo,

                CustomerName = bd.Borrow.Customer.CustomerName,
                LibrarianName = bd.Borrow.Librarian.LibrarianName,
                BookDescription = bd.Book.BookDescription,
            }).ToList();

            // Paginate the list
            int pageNumber = page ?? 1; // If no page number is specified, default to page 1
            int pageSize = 10; // Number of items per page

            var paginatedList = model.ToPagedList(pageNumber, pageSize);

            ViewBag.SearchTerm = searchTerm; // Pass the search term to the view for persistence in the search box
            return View(paginatedList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customer = _cus.GetAll();
            ViewBag.Customer = new SelectList(customer, "CustomerId", "CustomerName");
            //---------------------------------------------------------------------//

            var librarian = _lia.GetAll();
            ViewBag.librarians = new SelectList(librarian, "LibrarianId", "LibrarianName");
            //---------------------------------------------------------------------//

            var books = _book.GetAll();

            // Determine which books are borrowed
            var bookItems = books.Select(b => new SelectListItem
            {
                Value = b.BookId.ToString(),
                Text = b.BookCode,
                Selected = true
            });
            var availableBooks = bookItems.Where(b => !_service.IsBorrowedAndNotReturned(int.Parse(b.Value)));

            ViewBag.Book = availableBooks.ToList();

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
                TempData["ToastrMessage"] = "Borrow SuccessFully";
                TempData["ToastrMessageType"] = "success";
                return RedirectToAction("Index");
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
            ViewBag.Book = new SelectList(book, "BookId", "BookCode");


            var borrow = _service.Get(BorrowId, BorrowDetailId);
            if (borrow == null)
            {
                return NotFound();
            }

            return View("Edit", borrow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Borrow borrow, BorrowDetail borrowDetail)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            var result = _service.Updade(borrow, borrowDetail);
            if (result)
            {
                TempData["ToastrMessage"] = "Update SuccessFully";
                TempData["ToastrMessageType"] = "success";
                return RedirectToAction("Index", "BorrowDetial");
            }

            return View("Edit");
        }


        //make when check on the and book has return 
        [HttpPost]
        public IActionResult UpdateIsReturn(int BorrowDetailId)
        {
            // Fetch the current IsReturn value
            string query = "SELECT IsReturn FROM BorrowDetail WHERE BorrowDetailId = @BorrowDetailId";
            bool currentIsReturn = _dapper.Connection.QuerySingleOrDefault<bool>(query, new { BorrowDetailId });

            // Toggle the IsReturn value
            bool newIsReturn = !currentIsReturn;

            // Update the database with the new IsReturn value
            string updateQuery = "UPDATE BorrowDetail SET IsReturn = @IsReturn WHERE BorrowDetailId = @BorrowDetailId";
            _dapper.Connection.Execute(updateQuery, new { IsReturn = newIsReturn ? 1 : 0, BorrowDetailId });

            // Provide feedback to the user
            TempData["ToastrMessage"] = "Update Successful";
            TempData["ToastrMessageType"] = "success";

            return RedirectToAction("Index");
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

            return View("Delete", borrow); ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Destroy(int BorrowId, int BorrowDetailId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete");
            }
            var result = _service.Delete(BorrowId, BorrowDetailId);
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
    }
}
