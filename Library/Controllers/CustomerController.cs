using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            this._service = service;
        }
        public IActionResult Index()
        {
           var customer = _service.GetAll();
            return View("Index",customer);
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
