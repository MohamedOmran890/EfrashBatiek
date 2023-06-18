﻿using EfrashBatek.Models;
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

namespace EfrashBatek.Controllers
{
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
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();


			//...populate other properties
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


			//...populate other properties
			ViewBag.Model = user;
			return View(address.View());

        }

        public IActionResult CreateAddress()
        {
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();


			//...populate other properties
			ViewBag.Model = user;
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
			User user = identityRepository.GetUser();
			Customer customer = _customer.GetCustomerbyUserId();
			List<Order> orders = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orders.Count();


			//...populate other properties
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
            if (Isorder == true)
            {
                var items = cart.LoadFromCookie();

                var list = items.Where(i => i.CartID == cartID).ToList();
                int totalcost = 0;
                List<Order_Item> order_Items = new List<Order_Item>();
                // Get the first 8 characters of the GUID as a string
                Guid guid = Guid.NewGuid();
                string code = guid.ToString().Substring(0, 8);
                Order order = new Order();
                order.OrderCode = code;
                foreach (var item in list)
                {
                    item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
                    totalcost += item.Quantity * (int)item.Item.Price;
                  
                    Order_Item order_Item = new Order_Item();
                    // add item 
                    order_Item.item = item.Item;
                    order_Item.ItemID = item.ItemID;
                    order_Item.Quantity = item.Quantity;
                    order_Item.OrderState = OrderState.Delivering; 
                    // note : you should generate it by real shop ..
                    order_Item.Shop = context.Shops.FirstOrDefault();
                    order_Items.Add(order_Item);
                }

               

                
                order.TotalCost = totalcost;
                DateTime orderDate = DateTime.Now;

                order.OrderDate = orderDate.Date;
                order.PaymentMethod = PaymentMethod.CashOnDelivery; 

                order.AddressID = selectedAddressId; 
                order.Address = address.GetbyID(selectedAddressId);

                order.CustomerID = customer.Id;
             
                orderr.Create(order);
                Order  findorder = context.Orders.FirstOrDefault(i => i.OrderCode == code);
                foreach(var item in order_Items)
                {
                    item.OrderID = findorder.ID;
                   
                    order_ItemRepository.Create(item);  
                 
                }
       
                findorder.Order_Item = order_Items;

                context.Orders.Update(findorder);   
                context.SaveChanges();  

                cart.Clear();

            }
            
           
			


            orders = context.Orders.Where(i=>i.CustomerID ==  customer.Id).ToList();


		
			List<Order> orderss = context.Orders.Where(i => i.CustomerID == customer.Id).ToList();
			ViewBag.Orders = orderss.Count();

			User user = identityRepository.GetUser();
			//...populate other properties
			ViewBag.Model = user;


			if (customer.Orders.Count == 0) { 
                return View("GoShopping");
            }
            else
            {
                return View(orders);

            }
            
        }
        [HttpPost]
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


			//...populate other properties
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


			//...populate other properties
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




