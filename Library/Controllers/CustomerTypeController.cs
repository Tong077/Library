using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CustomerTypeController : Controller
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
        public IActionResult Create(CustomerType customerType)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int CustomerTypeId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(CustomerType customerType)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int CustomerTypeId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(CustomerType customerType)
        {
            return View();
        }
    }
}
