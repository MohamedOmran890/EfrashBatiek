using EfrashBatek.Models;
using EfrashBatek.service;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EfrashBatek.Controllers
{
    //[Authorize(Roles ="Admin")]
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
        private readonly IUserRepository userRepository;
        IAdminRepository adminRepository;

        public AdminController(UserManager<User>usermanager, ICustomerRepository customer,
            IOrderRepository order, IShopRepository shop, 
            IStaffRepository staff, IOrder_ItemRepository order_item, Context context
            ,EmailStaffService emailStaffService , IUserRepository userRepository,IAdminRepository adminRepository)
        {
            this.customer = customer;
            this.order = order;
            this.shop = shop;
            this._staff = staff;
            _userManager = usermanager;
            this.order_item = order_item;
            _context = context;
            EmailStaffService = emailStaffService;
            this.userRepository = userRepository;
            this.adminRepository = adminRepository;
        }
        // done - test 
        public IActionResult Index()
        {
			List<Order> orderss = _context.Orders.Where(i =>true).ToList();
			foreach (Order order in orderss)
			{
				Customer customerr = _context.Customers.FirstOrDefault(i => i.Id == order.CustomerID);
				User user = _context.Users.FirstOrDefault(i => i.Id == customerr.UserId);
				order.Customer = customerr;
			}
			var Dash = new DashboardViewModel
            {
                 TotalCustomers = customer.TotalCustomers(),
                 TotalOrders = order.TotalOrders(),
                 Totalshop=shop.TotalShop() ,
				orders = orderss 

			};
            return View(Dash);
        }

        // done test 
        public IActionResult Orders() {
            List<Order> orderss = _context.Orders.Where(i => true).ToList();
            foreach (Order order in orderss)
            {
                Customer customerr = _context.Customers.FirstOrDefault(i => i.Id == order.CustomerID);
                User user = _context.Users.FirstOrDefault(i => i.Id == customerr.UserId);
                order.Customer =customerr;
            }
            var Dash = new DashboardViewModel
            {
                orders = orderss
            };
            return View(Dash);
        
        }
        // done - test 
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(i => i.ID == id);
            var orderitems = _context.Order_Items.Where(i=>i.OrderID==order.ID).ToList();   
            foreach (var item in orderitems) { 
               _context.Order_Items.Remove(item);   
                _context.SaveChanges(); 
            }
            _context.Orders.Remove(order);  
            _context.SaveChanges();
			return RedirectToAction("Orders");

		}
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
            if (!ModelState.IsValid || ans == null)
            {
                if (ans == null)
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
                var roleName = "Seller";
                await _userManager.AddToRoleAsync(user, roleName);
                // Redirect the user to the login page
                _staff.Create(staff);
                  await EmailStaffService.SendEmail(model.Email, model.Username, model.Password, model.FirstName);
                return Content("Done");
            }

            //if(check==null)
            //ModelState.AddModelError("","Not Found ShopNumber ");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }

            return View("AddStaff", model);
        }




        public IActionResult AddShop()
        {
            Shop shop = new Shop();
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
                return View("Index");
            }
            else
                return View("AddShop");

        }
        public IActionResult SellerDetails()
        {
            List<Staff> staffs = _context.The_Staff.Where(i => true).ToList();
            foreach(var staff in staffs)
            {
                User user = _context.Users.FirstOrDefault(i => i.Id == staff.UserId);
                staff.User = user;
                Shop shop = _context.Shops.FirstOrDefault(i => i.ID == staff.ShopID);
                staff.Shop = shop;
            }
            return View(staffs);

        }
        public IActionResult DeleteSeller(int id) {

            Staff staff = _context.The_Staff.FirstOrDefault(i => i.ID == id);
            _context.The_Staff.Remove(staff);
            _context.SaveChanges();

            return RedirectToAction("SellerDetails");



        }
        [HttpPost]
        public IActionResult EditSeller(string id)
        {

            User staff = _context.Users.FirstOrDefault(i => i.Id == id);

            return View(staff);




        }
        [HttpPost]
        public IActionResult EditSellerr (User user )
        {



            userRepository.Update(user);


            return RedirectToAction("SellerDetails");





        }
        public IActionResult ShopDetails()
        {
            var ans = shop.GetAll();
            return View(ans);

        }
        public IActionResult EditShop(int id)
        {
            var ans = shop.GetById(id);
            return View(ans);
        }
        [HttpPost]
        public IActionResult SaveEditShop(int id, Shop obj)
        {
            var ans = shop.Update(id, obj);
            return RedirectToAction("ShopDetails");
        }

        public IActionResult DeleteShop(int id)
        {
            int valid = shop.Delete(id);
            if (valid != 0)
                return RedirectToAction("ShopDetails");
            else
                return Content("Error Try Again");

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
            if (!ModelState.IsValid)
            {
             
                return View(model);
            }
          
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    age = model.Age,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.Birthdate,
                    Gender = (Gender)model.Gender,
                };
                var admin = new Admin
                {
                    UserId = user.Id,
                };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleName = "Admin";
                await _userManager.AddToRoleAsync(user, roleName);
                // Redirect the user to the login page
                adminRepository.Create(admin);
                //wait EmailStaffService.SendEmail(model.Email, model.Username, model.Password, model.FirstName);
                await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                return RedirectToAction("Index");
            }

            //if(check==null)
            //ModelState.AddModelError("","Not Found ShopNumber ");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }
            return View(model);

        }






    }
}
