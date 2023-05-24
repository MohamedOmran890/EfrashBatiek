using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
