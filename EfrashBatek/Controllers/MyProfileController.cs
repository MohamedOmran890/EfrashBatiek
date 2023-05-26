using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using EfrashBatek.service;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace EfrashBatek.Controllers
{
	public class MyProfileController : Controller
	{
		private readonly Context _context;
		ICustomerRepository User;
		public MyProfileController(Context context)
		{
			_context = context;
			User = new CustomerRepository(_context);
		}


		// integer or string??  { id of user or customer or identity) ???
		// error beacause data not founded ..
		// there is not id with 1 ..
		[HttpGet]
		public IActionResult UpdateProfile(string  id )
		{
			Customer customer =   User.GetById(id);	

			
			return View(customer);
		}

		[HttpPost]
		// id  --> from URL ..
		// obj --> from post method 
		public IActionResult SaveChangesProfile([FromRoute] int id, Customer obj)
		{

			// update service customer doesn't found  and access
			if(ModelState.IsValid) {
				


			}

			return RedirectToAction("UpdateProfile"); // redirect to action or view ?? 
		}
		public IActionResult UpdateAddress(int id )
		{
			Customer obj = User.GetById(id);
			return View(obj );
		}
		public IActionResult SaveChangesAddress([FromRoute] int id, Customer obj)
		{
			// update service customer doesn't found and access 

			return View();
		}
		public IActionResult ViewOrders(int id )
		{

			
			if (id == 1)
			{

				return View("GoShopping");
			}
			else
			{
				return View();
			}



		}
		public IActionResult OrderDetails()
		{

			return View();
		}
		public IActionResult ViewGuaraneet()
		{
			return View();
		}

		public IActionResult PasswordSetting()
		{
			return View();

		}
		public IActionResult Logout()
		{
			return View();
		}


	}
}
