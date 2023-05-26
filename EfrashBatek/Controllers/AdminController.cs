using EfrashBatek.Models;
using EfrashBatek.service;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class AdminController : Controller
    {
        ICustomerRepository customer;
        IOrderRepository order;
        IShopRepository shop;
        public AdminController(ICustomerRepository customer, IOrderRepository order, IShopRepository shop)
        {
            this.customer = customer;
            this.order = order;
            this.shop = shop;
        }
        public IActionResult Dashboard()
        {
            var Dash = new DashboardViewModel
            {
                 TotalCustomers = customer.TotalCustomers(),
                 TotalOrders = order.TotalOrders(),
                 Totalshop=shop.TotalShop()
            };
            return View(Dash);
        }
        public IActionResult AddStaff()
        {
            return View();
        }
        public IActionResult SaveStaff(Shop obj)
        {
            if (ModelState.IsValid)
            {
                shop.Create(obj);
                return RedirectToAction("Dashboard");
            }
            else
                return View("AddStaff");

        }
        public IActionResult AddShop()
        {
            return View();

        }
        [HttpPost]
        public IActionResult SaveShop(Shop obj)
        {
            if (ModelState.IsValid)
            {
                shop.Create(obj);
            return View("Dashboard");
            }
            else
                return View("AddShop");

        }
        public IActionResult ShopDetails()
        {
            var ans = shop.GetAll();
            return View(ans);

        }
        public IActionResult EditShop(int id)
        {
            var ans=shop.GetById(id);
            return View(ans);
        }
        [HttpPost]
        public IActionResult SaveEditShop(int id,Shop obj)
        {
            var ans = shop.Update(id,obj);
            return RedirectToAction("ShopDetails");
        }

        public IActionResult DeleteShop(int id)
        {
            int valid=shop.Delete(id);
            if (valid != 0)
                return RedirectToAction("ShopDetails");
            else
                return Content("Error Try Again");

        }



    }
}
