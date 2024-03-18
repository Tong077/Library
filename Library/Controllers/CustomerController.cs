using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CustomerController : Controller
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
        public IActionResult Create(Customer customer)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int CustomerId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int CustomerId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Destroy(int customerId)
        {
            return View();
        }
    }
}
