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
        IOrder_ItemRepository _itemRepository;
        public AdminController(UserManager<User>usermanager, ICustomerRepository customer,
            IOrderRepository order, IShopRepository shop, IStaffRepository staff, IOrder_ItemRepository itemRepository)
        {
            this.customer = customer;
            this.order = order;
            this.shop = shop;
            this._staff = staff;
            _userManager = usermanager;
            _itemRepository = itemRepository;
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
        [HttpPost]
        public async Task<IActionResult> SaveStaff(RegisterViewModelStaff model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Phone,
                age = model.Age,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.Birthdate,
                Gender = (Gender)model.Gender,

            };

        var staff = new Staff
            {
                UserId = user.Id,
                ShopID = model.TaxCardNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);//Created Cookies
            var check = shop.GetById(model.ShopNumber);
            if (result.Succeeded&&check!=null)
            {
                // Add user to default role
                //  await _userManager.AddToRoleAsync(user, "User");

                // Redirect the user to the login page
                _staff.Create(staff);
                return Content("Done");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                if(check==null)
                ModelState.AddModelError("","Not Found ShopNumber ");

            }

            return View("AddStaff",model);
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
        public IActionResult Orders()
        {
            var ans = order.GetByShop();
            return View(ans);
        }




    }
}
