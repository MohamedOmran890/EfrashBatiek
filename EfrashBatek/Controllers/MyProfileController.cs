using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EfrashBatek.Controllers
{
	public class MyProfileController : Controller
	{
		private readonly Context _context;
		ICustomerRepository User ;
		public MyProfileController(Context context , ICustomerRepository User )
		{
			_context = context;
			this.User = User;	

		}


		[HttpGet]
		public IActionResult UpdateProfile(int CustomerID )
		{
			//Customer obj = User.GetById(id);
			User user = new User { Id = "11111", FirstName = "alyaa", LastName = "elhawary", Email = "alyaamamoon@gmail.com", PhoneNumber = "01111111" };
			Customer customer = new Customer { Id = 1, User = user };
			return View(customer );
		}

		[HttpPost]
		
		public IActionResult SaveChangesProfile([FromRoute] int id, Customer obj)
		{

			if(ModelState.IsValid) {


				User.Edit(obj, id); 
			
	
			}
			return RedirectToAction("UpdateProfile" , id); // redirect to action or view ?? 
		}
		public IActionResult UpdateAddress(int id )
		{
			//Customer obj = User.GetById(id);

			
		   Address address = new Address { FirstName = "efef", LastName = "fefe", City = "rfrfrf"  , phone="rfrfrf"};
			User user = new User {Address=address ,  Id = "11111", FirstName = "alyaa", LastName = "elhawary", Email = "alyaamamoon@gmail.com", PhoneNumber = "01111111" };

			Customer customer  = new Customer { Id = 1, User =user  };
			
			return View(customer);
		}
		public IActionResult SaveChangesAddress([FromRoute] int id, Customer obj)
		{
			// update service customer doesn't found and access 

			if (ModelState.IsValid)
			{

			   User.Edit(obj, id);

				
			}
			return RedirectToAction("UpdateAddress", id); // redirect to action or view 
		}
		public IActionResult ViewOrders(int id )
		{
            //ICollection<Order> orders = User.GetOrders(id); // expection error 
            Order order = new Order();
			order.OrderDate = new DateTime(2023, 5, 31);
            order.OrderCode = "#125456";
			order.TotalCost = 333;
			order.ID = 3;
            List<Order> orders = new List<Order>() { order, order, order, order };
			

			if (id==1 )
			{

				return View("GoShopping");
			}
			else
			{
				return View(orders);
			}



		}
        
        public IActionResult OrderDetails([FromRoute] int id ) // get from link 
		{
			// --> send this order to view .. 
			//Order order =  User.getOrder(id , id);

			// Testing 
			Order order= new Order();
			order.OrderDate = new DateTime(2023, 5, 31);
			order.OrderCode = " #131321";
			order.PaymentMethod = PaymentMethod.CashOnDelivery; // doesn't work 
            order.TotalCost = 7000;
            // shipping cost total 
            Item item = new Item();
            item.Name = "Bedroom";


            item.Image = "1.jpg";
            item.Image2 = "1.jpg";
            item.Price = 78;
            item.discount = "%15";

            item.PriceAfterSale = 48;
			Order_Item item2 = new Order_Item();
			item2.ID = 2322;
			item2.OrderID= id;	
			item2.item = item;
			item2.Quantity = 2;
			
			item2.OrderState = OrderState.Delivering; // problem 
			List<Order_Item> items = new List<Order_Item>();
			items.Add(item2);
			items.Add(item2);
			order.Order_Item = items;
		
			// customer testing 
	
			Address address = new Address();
			address.description = "Dreams Land , Dreams Schools";
			address.phone = "1004443226";
			address.FirstName = "Hazem ";
			address.LastName = "Abdelstatter";
			ViewData["Address"] = address;
		    
            return View(order);
		}
		public IActionResult Warrently()
		{
			return View();
		}

		public IActionResult UpdatePassword(int id)
		{
			//Customer customer = User.GetById(id);	
		
			return View();

		}
		public IActionResult Logout()
		{
			return View();
		}


	}
}
