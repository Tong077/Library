using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BorrowController : Controller
    {
        private readonly IBorrowService _service;
        public BorrowController(IBorrowService service)
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
        public IActionResult Create(Borrow borrow)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int BorrowId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Borrow borrow)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int BorrowId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destroy(int borrowId)
        {
            return View();
        }
    }
}
