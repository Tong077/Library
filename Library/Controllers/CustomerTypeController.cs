using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CustomerTypeController : Controller
    {
        private readonly ICustomerService _service;
        public CustomerTypeController(ICustomerService service)
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
        public IActionResult Destroy(int customerTypeId)
        {
            return View();
        }
    }
}
