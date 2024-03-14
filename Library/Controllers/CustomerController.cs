using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
