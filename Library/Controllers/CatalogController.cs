using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
