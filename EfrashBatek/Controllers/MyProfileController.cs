using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using System.Net.Sockets;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Authorization;

namespace EfrashBatek.Controllers
{
    [Authorize]
	public class MyProfileController : Controller
	{
		private readonly Context context;
        private readonly IOrder_ItemRepository order_ItemRepository;
        IUserRepository User;
        private readonly ICustomerRepository _customer;
        private readonly IIdentityRepository identityRepository;
        private readonly IAddressRepository address;
        private readonly IOrderRepository orderr;
        private readonly SignInManager<User> signInManager;
		private readonly ICartRepository cart;

		public UserManager<User> UserManager { get; set; }
        
		public MyProfileController(Context context, IOrder_ItemRepository   order_ItemRepository,  IUserRepository User, ICustomerRepository customer ,  IIdentityRepository identityRepository , IAddressRepository address , IOrderRepository order , SignInManager<User> signInManager , ICartRepository cart)
		{
			this.context = context;
            this.order_ItemRepository = order_ItemRepository;
            this.User = User;
            _customer = customer;
            this.identityRepository = identityRepository;
            this.address = address;
             orderr = order;
            this.signInManager = signInManager;
			this.cart = cart;
		}

        public IActionResult ViewCustoms()
        {
            User user = identityRepository.GetUser();
            Customer customer = _customer.GetCustomerbyUserId();
            List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
            ViewBag.Orders = orders.Count();

            ViewBag.Model = user;
            var Customs = context.Customs.Where(i=>i.CustomerID ==  customer.Id).ToList();  

            return View(Customs);  
        }
    
        public IActionResult ViewCustomss(int id )
        {
           var model =  context.Customs.FirstOrDefault(i=>i.ID == id);  

            return View("CustomDetails" , model);
        }
        [HttpGet]

		public async Task<ActionResult> UpdateProfile()
		{
			
			var HaveSession = HttpContext.Session.GetString("Id");

			if (HaveSession == null)
			{
				return RedirectToAction("Login", "Account");

			}
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
            if (customer != null)
            {
                List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
                ViewBag.Orders = orders.Count();
            }

			ViewBag.Model = user;


			return View(user);


		}

		[HttpPost]

		public IActionResult UpdateProfile(User New)
		{

			User.Update(New);



			return RedirectToAction("UpdateProfile", New);
		}
		public IActionResult ViewAddress() {

			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();
			ViewBag.Model = user;
			return View(address.View());

        }

        public IActionResult CreateAddress()
        {
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();
			ViewBag.Model = user;
			return View();

        }
        [HttpPost]

        // save changes 
        public IActionResult CreateAddress(Address New , int idd)
        {

            address.Create(New);

            if(idd == 3)
            {
                return RedirectToAction("DefaultAddress", "Checkout");
			}
            return RedirectToAction("ViewAddress", "MyProfile");


        }

		[HttpPost]
		public IActionResult ViewAddressDetails(int id)
		{
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();
			ViewBag.Model = user;
			var Address = address.GetbyID(id);

			return View("EditAddress", Address);

		}
		[HttpPost]

        public IActionResult EditAddress(Address New)
        {

            address.Edit(New);

            return RedirectToAction("ViewAddress", "MyProfile");

        }
        [HttpPost]
        public IActionResult DeleteAddress(int id )
        {
            address.Delete(id);

            return RedirectToAction("ViewAddress", "MyProfile");
        }

        public IActionResult ViewOrders(int cartID , int selectedAddressId  , bool Isorder)
		{
            List<Order> orders = new List<Order>();
            Customer customer = _customer.GetCustomerbyUserId();

            orders = context.Orders.Where(i=>i.CustomerID ==  customer.Id).ToList();


		
			List<Order> orderss = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orderss.Count();

			User user = identityRepository.GetUser();
			//...populate other properties
			ViewBag.Model = user;


			if (customer.Orders == null ) { 
                return View("GoShopping");
            }
            else
            {
                return View(orders);

            }
            
        }
    
        public IActionResult OrderDetails(int id )
        {
            Order order =  context.Orders.FirstOrDefault(i=>i.ID == id ); 
            var list =  context.Order_Items.Where(i=>i.OrderID == order.ID).ToList();
            var addresss =  address.GetbyID(order.AddressID); 
            order.Address =addresss;
            foreach(var item in list )
            {
                item.item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            }
            order.Order_Item = list;
              
            return View(order);
        }

        public IActionResult UpdatePassword()
        {
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();

			ViewBag.Model = user;
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ChangePasswordVM model)
        {
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();
			ViewBag.Model = user;
			if (ModelState.IsValid)
            {
                var userr = identityRepository.GetUser();
                if (userr == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var result = await UserManager.ChangePasswordAsync(userr, model.CurrentPassword, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    await signInManager.RefreshSignInAsync(userr);
                    return RedirectToAction("Login", "Account");

                }
                foreach (var er in result.Errors)
                {
                    ModelState.AddModelError("", er.Description);
                }

            }
            return View(model);

        }

     





    }
}




