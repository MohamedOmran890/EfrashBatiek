//using EfrashBatek.Models;
//using EfrashBatek.service;
//using Microsoft.AspNetCore.Mvc;
//using EfrashBatek.service;
//using System.Linq;
//using System;
//using Microsoft.AspNetCore.Identity;

//namespace EfrashBatek.Controllers
//{
//	public class MyProfileController : Controller
//	{
//		private readonly Context _context;
//		ICustomerRepository User;
//		public MyProfileController(Context context)
//		{
//			_context = context;
//			User = new CustomerRepository(_context);
//		}


//		// integer or string??  { id of user or customer or identity) ???
//		// error beacause data not founded ..
//		// there is not id with 1 ..
//		/*[HttpGet]
//		public IActionResult UpdateProfile(string  id )
//		{
//			Customer customer =   User.GetById(id);	


//			return View(customer);
//		}*/

//		[HttpPost]
//		// id  --> from URL ..
//		// obj --> from post method 
//		public IActionResult SaveChangesProfile([FromRoute] int id, Customer obj)
//		{

//			// update service customer doesn't found  and access
//			if(ModelState.IsValid) {



//			}

//			return RedirectToAction("UpdateProfile"); // redirect to action or view ?? 
//		}
//		public IActionResult UpdateAddress(int id )
//		{
//			Customer obj = User.GetById(id);
//			return View(obj );
//		}
//		public IActionResult SaveChangesAddress([FromRoute] int id, Customer obj)
//		{
//			// update service customer doesn't found and access 

//			return View();
//		}
//		public IActionResult ViewOrders(int id )
//		{


//			if (id == 1)
//			{

//				return View("GoShopping");
//			}
//			else
//			{
//				return View();
//			}



//		}
//		public IActionResult OrderDetails()
//		{

//			return View();
//		}
//		public IActionResult ViewGuaraneet()
//		{
//			return View();
//		}

//		public IActionResult PasswordSetting()
//		{
//			return View();

//		}
//		public IActionResult Logout()
//		{
//			return View();
//		}


//	}
//}
/*Omran Last*/
/*Alyaa*/
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
        ICustomerRepository User;
        public MyProfileController(Context context, ICustomerRepository User)
        {
            _context = context;
            this.User = User;

        }


        [HttpGet]
        public IActionResult UpdateProfile(int id)
        {
            Customer obj = User.GetById(id);
            User user = new User { Id = "11111", FirstName = "alyaa", LastName = "elhawary", Email = "alyaamamoon@gmail.com", PhoneNumber = "01111111" };
            Customer customer = new Customer { Id = 1, User = user };
            return View(customer);
        }

        [HttpPost]

        //public IActionResult SaveChangesProfile([FromRoute] int id, Customer obj)
        //{

        //    if (ModelState.IsValid)
        //    {


        //        User.Edit(obj, id);


        //    }
        //    return RedirectToAction("UpdateProfile", id); // redirect to action or view ?? 
        //}
        //public IActionResult UpdateAddress(int id)
        //{
        //    Customer obj = User.GetById(id);


        //    Address address = new Address { FirstName = "efef", LastName = "fefe", City = "rfrfrf", phone = "rfrfrf" };
        //    User user = new User { Address = address, Id = "11111", FirstName = "alyaa", LastName = "elhawary", Email = "alyaamamoon@gmail.com", PhoneNumber = "01111111" };

        //    Customer customer = new Customer { Id = 1, User = user };

        //    return View(customer);
        //}
        //public IActionResult SaveChangesAddress([FromRoute] int id, Customer obj)
        //{
        //    // update service customer doesn't found and access 

        //    if (ModelState.IsValid)
        //    {

        //        User.Edit(obj, id);


        //    }
        //    return RedirectToAction("UpdateAddress", id); // redirect to action or view 
        //}
        public IActionResult ViewOrders(int id)
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
        public IActionResult Warrently()
        {
            return View();
        }

        public IActionResult UpdatePassword(int id)
        {
            Customer customer = User.GetById(id);

            return View();

        }
        public IActionResult Logout()
        {
            return View();
        }


    }
}