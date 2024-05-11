using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _service;
        private readonly IBorrowService _borrow;
        private readonly IBorrowDetailService _bodetail;
        public HomeController(IBookService service, IBorrowService borrow, IBorrowDetailService bodetail)
        {
            _service = service;
            _borrow = borrow;
            _bodetail = bodetail;
        }

        public IActionResult Index()
        {

            var  book = _service.GetAll();

            int bookcount = book.Count();
            ViewData["BookCount"] = bookcount;
           //------------------------------------------------------------//


            var borrow = _borrow.GetAll();
            int borrowcount = borrow.Count();
            ViewData["BorrowCount"] = borrowcount;

            //--------------------------------------------------------//

            var detail = _bodetail.GetAll();
            decimal IsReturn = detail.Count();

            ViewData["IsReturn"] = IsReturn;

            return View("Index", book);
        }
    }
}
