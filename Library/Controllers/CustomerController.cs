using Dapper;
using Library.Data;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DapperDbConnext _dapperDb;
        private readonly ICustomerService _service;
        private readonly ICustomerTypeService _ctx;
        public CustomerController(ICustomerService service, ICustomerTypeService ctx, DapperDbConnext dapperDb)
        {
            _service = service;
            _ctx = ctx;
            _dapperDb = dapperDb;
        }

        public IActionResult Index()
        {
            var customers = _service.GetAll().ToList();

            return View("Index",customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customertype = _ctx.GetAll();
            ViewBag.customerTypes = new SelectList(customertype, "CustomerTypeId", "CustomerTypeName");
            return View();
        }
        [HttpPost]
        public IActionResult Store(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            var customers = _service.Create(customer);
            if (customers)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int CustomerId)
        {
            var customertype = _ctx.GetAll();
            ViewBag.customerTypes = new SelectList(customertype, "CustomerTypeId", "CustomerTypeName");


            var customer = _service.Get(CustomerId);
			return View("Edit", customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", customer);
            }
            var result = _service.Update(customer);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", customer);
        }

        [HttpGet]
        public IActionResult Delete(int CustomerId)
        {
            var customertype = _ctx.GetAll();
            ViewBag.customerTypes = new SelectList(customertype, "CustomerTypeId", "CustomerTypeName");

            var cus = _service.Get(CustomerId);
            return View("Delete",cus);
        }

        [HttpPost]
        public IActionResult Destroy(int customerId)
        {
            if(!ModelState.IsValid)
            {
                return View("Delete",customerId);
            }
            var result = _service.Delete(customerId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            
            return View("Delete",customerId);
        }

        [HttpPost]
        public IActionResult Hidden(int customerId, bool isHidden)
        {
            var customer = _service.Get(customerId);
            if(customer != null)
            {
                customer.IsHidden = isHidden;
                _service.Update(customer);
            }
            return RedirectToAction("Index");
        }
    }
}
