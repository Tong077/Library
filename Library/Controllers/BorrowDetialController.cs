using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BorrowDetialController : Controller
    {
        private readonly IBorrowDetailService _service;
        public BorrowDetialController(IBorrowDetailService service)
        {
            _service = service;
        }
		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BorrowDetail borrowdetail)
        {
            return View();
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
