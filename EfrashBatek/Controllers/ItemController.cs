using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
