using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service)
        {
            _service = service;
        }
		public IActionResult Index()
        {
            var catalog = _service.GetAll();

            return View("Index", catalog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Store(Catalog catalog)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", catalog);
            }
            var result = _service.Create(catalog);
            if (result)
            {
                return RedirectToAction("Index");
            }
                return View("Create",catalog);
        }

        [HttpGet]
        public IActionResult Edit(int CatalogId)
        {
            var catalog = _service.Get(CatalogId);
            return View("Edit", catalog);
        }

        [HttpPost]
        public IActionResult Update(Catalog catalog)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit", catalog);
            }
            var result = _service.Update(catalog);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", catalog);
        }

        [HttpGet]
        public IActionResult Delete(int CatalogId)
        {
            var catalog = _service.Get(CatalogId);

            return View("Delete", catalog);
        }
        [HttpPost]
        public IActionResult Destroy(int CatalogId)
        {
            if (!ModelState.IsValid)
            {
                return View("Delete", CatalogId);
            }
            var result = _service.Delete(CatalogId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", CatalogId);
        }
    }
}
