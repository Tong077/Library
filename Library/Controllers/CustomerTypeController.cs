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
            return View("Edit", customertype);
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

            var customertype = _service.Get(CustomerTypeId);
            return View("Delete", customertype);
        }
        [HttpPost]
        public IActionResult Destroy(int customerTypeId)
        {
            if (!ModelState.IsValid)
            {
                return View(customerTypeId);
            }
            var result = _service.Delete(customerTypeId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(customerTypeId);
        }
    }
}
