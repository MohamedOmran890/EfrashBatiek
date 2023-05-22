using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class CustomerController : Controller, ICustomer
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
