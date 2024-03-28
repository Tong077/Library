using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class CustomerTypeController : Controller
    {
       
        private readonly ICustomerTypeService _service;
        public CustomerTypeController(ICustomerTypeService service )
        {
            _service = service;
           
        }

		public IActionResult Index()
        {
            var customes = _service.GetAll();
            return View("Index",customes);
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(CustomerType customerType)
        {
            if(!ModelState.IsValid)
            {
                return View(customerType);
            }
            var result = _service.Create(customerType);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(customerType);
        }

        [HttpGet]
        public IActionResult Edit(int CustomerTypeId)
        {
            var customertype = _service.Get(CustomerTypeId);
            return View("Edit",CustomerTypeId);
        }

        [HttpPost]
        public IActionResult Update(CustomerType customerType)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit",customerType);
            }
            var result = _service.Update(customerType);
            if (result) 
            {
                return RedirectToAction("Index");
            }
           
            return View("Edit",customerType);
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
