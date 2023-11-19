using Microsoft.AspNetCore.Mvc;

namespace University.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
