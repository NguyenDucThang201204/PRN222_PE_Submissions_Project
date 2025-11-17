using Microsoft.AspNetCore.Mvc;

namespace PRN232_FA25_SE173699.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
