using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BorrowDetialController : Controller
    {
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
        public IActionResult Delete(BorrowDetail borrowdetail)
        {
            return View();
        }
    }
}
