using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LibrarianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
