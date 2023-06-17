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

namespace EfrashBatek.Controllers
{
	public class MyProfileController : Controller
	{
		private readonly Context context;
		IUserRepository User;

		private readonly IIdentityRepository identityRepository;
        private readonly IAddressRepository address;
        private readonly IOrderRepository order;
        private readonly SignInManager<User> signInManager;
		private readonly ICartRepository cart;

		public UserManager<User> UserManager { get; set; }
        
		public MyProfileController(Context context, IUserRepository User, IIdentityRepository identityRepository , IAddressRepository address , IOrderRepository order , SignInManager<User> signInManager , ICartRepository cart)
		{
			this.context = context;
			this.User = User;

			this.identityRepository = identityRepository;
            this.address = address;
            this.order = order;
            this.signInManager = signInManager;
			this.cart = cart;
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

			return View(user);


		}

		[HttpPost]

		public IActionResult UpdateProfile(User New)
		{

			User.Update(New);



			return RedirectToAction("UpdateProfile", New);
		}
		public IActionResult ViewAddress() {

            return View(address.View());

        }

        public IActionResult CreateAddress()
        {
            return View();

        }
        [HttpPost]

        // save changes 
        public IActionResult CreateAddress(Address New)
        {

            address.Create(New);

            return RedirectToAction("ViewAddress", "MyProfile");


        }

		[HttpPost]
		public IActionResult ViewAddressDetails(int id)
		{
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

        public IActionResult ViewOrders(int cartID)

		{
			var items = cart.LoadFromCookie();
			var list = items.Where(i => i.CartID == cartID).ToList();
            int totalcost = 0;
            List<Order_Item> order_Items = new List<Order_Item>();  
			foreach (var item in list)
			{
				item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
                totalcost = item.Quantity * (int)item.Item.Price;
				Order_Item order_Item = new Order_Item();
                order_Item.item = item.Item;
                order_Items.Add(order_Item);    
			}
			User user = identityRepository.GetUser();

            List<Order> orders = new List<Order>();
                Order order = new Order();
            order.TotalCost =  totalcost;
            order.OrderDate = DateTime.Now;
            order.Order_Item  = order_Items;


            
           orders.Add(order);   

            
            if (orders.Count == 0) { 
                return View("GoShopping");
            }
            else
            {
                return View(orders);

            }
            
        }


        public IActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = identityRepository.GetUser();
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var result = await UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    await signInManager.RefreshSignInAsync(user);
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




