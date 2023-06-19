using EfrashBatek.Models;
using EfrashBatek.service;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EfrashBatek.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ICustomerRepository customer;
        IOrderRepository order;
        IShopRepository shop;
        IStaffRepository _staff;
        private readonly UserManager<User> _userManager;
        IOrder_ItemRepository order_item;
        Context _context;
        EmailStaffService EmailStaffService;
        public AdminController(UserManager<User>usermanager, ICustomerRepository customer,
            IOrderRepository order, IShopRepository shop, 
            IStaffRepository staff, IOrder_ItemRepository order_item, Context context
            ,EmailStaffService emailStaffService)
        {
            this.customer = customer;
            this.order = order;
            this.shop = shop;
            this._staff = staff;
            _userManager = usermanager;
            this.order_item = order_item;
            _context = context;
            EmailStaffService = emailStaffService;
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
                   return View(model);

        }


        [AllowAnonymous]
        public IActionResult AddStaff()
        {
            return View();
        }
        //Fake
        public IActionResult AddStaff2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveStaff(RegisterViewModelStaff model)
        {
            var ans = _context.Shops.FirstOrDefault(x => x.TaxCardNumber == model.ShopNumber);
            if (!ModelState.IsValid||ans==null)
            {
                if(ans==null)
                    ModelState.AddModelError("", "ShopNumber Not Found ");
                return View("AddStaff", model);
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
                  ShopID = ans.ID
            };
            //var check = shop.GetById(ans.ID);
            //if(check==null)
            //{
            //    return View("AddStaff", model);

            //}
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Add user to default role
                //  await _userManager.AddToRoleAsync(user, "User");

                // Redirect the user to the login page
                _staff.Create(staff);
              //  await EmailStaffService.SendEmail(model.Email, model.Username, model.Password, model.FirstName);
                return Content("Done");
            }

                //if(check==null)
                //ModelState.AddModelError("","Not Found ShopNumber ");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }

            return View("AddStaff",model);
        }


          

    public IActionResult AddShop()
        {
            Shop shop=new Shop();
            return View(shop);

        }
        public IActionResult AddShop2()
        {
            Shop shop = new Shop();
            return View(shop);

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
            var ans = order.GetAll();
            return View(ans);
        }



    }
}
