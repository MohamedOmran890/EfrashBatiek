using EfrashBatek.Models;
using EfrashBatek.service;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EfrashBatek.Controllers
{
    public class AdminController : Controller
    {
        ICustomerRepository customer;
        IOrderRepository order;
        IShopRepository shop;
        IStaffRepository _staff;
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User>usermanager,ICustomerRepository customer, IOrderRepository order, IShopRepository shop, IStaffRepository staff)
        {
            this.customer = customer;
            this.order = order;
            this.shop = shop;
            this._staff = staff;
            _userManager = usermanager;
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
         public async Task<IActionResult> SaveStaff(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("AddStaff");
        }

        var user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            PhoneNumber=model.Phone,
            age=model.Age,
            FirstName=model.FirstName,
            LastName=model.LastName,
            Gender= (Gender)model.Gender,

        };
            var staff = new Staff
            {
                UserId = user.Id,
                ShopID = model.ShopNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
               _staff.Create(staff);
            // Add user to default role
            await _userManager.AddToRoleAsync(user, "Staff");
            // Redirect the user to the login page
            return RedirectToAction("Dashboard");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

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
