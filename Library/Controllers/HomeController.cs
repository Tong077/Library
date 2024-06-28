using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _service;
        private readonly IBorrowService _borrow;
        private readonly IBorrowDetailService _borrowdetal;
        private readonly IBorrowDetailService _bodetail;
        private readonly ICustomerService _customer;
        public HomeController(IBookService service, IBorrowService borrow, IBorrowDetailService bodetail, IBorrowDetailService borrowdetal, ICustomerService customer)
        {
            _service = service;
            _borrow = borrow;
            _bodetail = bodetail;
            _borrowdetal = borrowdetal;
            _customer = customer;
        }

        public IActionResult Index()
        {

            var book = _service.GetAll();

            int bookcount = book.Count();
            ViewData["BookCount"] = bookcount;
            //------------------------------------------------------------//

           

            var borrowdetail = _borrowdetal.GetAll();
            int borrowcount = borrowdetail.Count();
            ViewData["BorrowCount"] = borrowcount;

            //--------------------------------------------------------//

            var customer = _customer.GetAll();
            int customercount = customer.Count();
            ViewData["CustomerCount"] = customercount;

            return View("Index");
        }
        [HttpGet]
        public JsonResult GetChartData()
        {
            // Get counts from services
            int bookCount = _service.GetAll().Count();
            int borrowCount = _borrow.GetAll().Count();
            int isReturnCount = _bodetail.GetAll().Count();

            // Prepare data object to return as JSON
            var data = new
            {
                labels = new[] { "Books", "Borrows", "Returns" },
                datasets = new[]
                {
                new
                {
                    label = "Count",
                    data = new[] { bookCount, borrowCount, isReturnCount },
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",   // Background color for Books
                        "rgba(54, 162, 235, 0.2)",   // Background color for Borrows
                        "rgba(255, 206, 86, 0.2)"    // Background color for Returns
                    },
                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",    // Border color for Books
                        "rgba(54, 162, 235, 1)",    // Border color for Borrows
                        "rgba(255, 206, 86, 1)"     // Border color for Returns
                    },
                    borderWidth = 1
                }
            }
            };

            return Json(data);
        }

    }
}

