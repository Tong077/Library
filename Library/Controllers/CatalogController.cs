using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
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
        public IActionResult Create(Catalog catalog)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int CatalogId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Catalog catalog)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int CatalogId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destroy(int CatalogId)
        {
            return View();
        }
    }
}
