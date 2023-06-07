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
        private readonly Context _context;
        IUserRepository User;
     
        private readonly IIdentityRepository identityRepository;    
        public UserManager<User> UserManager { get; set; }

        public MyProfileController(Context context, IUserRepository User , IIdentityRepository identityRepository)
        {
            _context = context;
            this.User = User;
         
            this.identityRepository = identityRepository;   
        }


        [HttpGet]

        public async Task<ActionResult>  UpdateProfile()
        {
            var HaveSession = HttpContext.Session.GetString("Id");
            //     _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));
            //  var userEmail =  User.FindFirstValue(ClaimTypes.Email)
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");

            }






            User user = identityRepository.GetUser();


            UserCustomerModel userCustomerModel = new UserCustomerModel();
            if (user != null)
            {
                userCustomerModel.PhoneNumber = user.PhoneNumber;
                userCustomerModel.LastName = user.LastName;
                userCustomerModel.FirstName = user.FirstName;
                userCustomerModel.Email = user.Email;
                userCustomerModel.Id = identityRepository.GetUserID();
            }
             return View(userCustomerModel);
          

        }

        [HttpPost]

        public IActionResult UpdateProfile(int test ,  UserCustomerModel New)

		{
            User CustomerUser =  new User();    
            CustomerUser.FirstName = New.FirstName;    
            CustomerUser.LastName = New.LastName;
            CustomerUser.Email = New.Email;   
            CustomerUser.PhoneNumber = New.PhoneNumber;   
            User.edit(CustomerUser , New.Id);


           
            return RedirectToAction("UpdateProfile", CustomerUser); // redirect to action or view ?? 
        }
  //      public async Task<ActionResult>  UpdateAddress()
  //      {
  //         AddressVM Address = new AddressVM(); 

		//	var username = contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Name);
		//	User user = await UserManager.FindByNameAsync(username);

  //          // get all zones in list .. 
  //           List<string> zones = new List<string>(); 
		//	foreach (string name in Enum.GetNames(typeof(Zone)))
		//	{
  //              zones.Add(name);
				
		//	}
		//	ViewData["ProductName"] = new SelectList();



		//}
      
        //public IActionResult ViewOrders(int id)
        //{
            
        //    Order order = new Order();
        //    order.OrderDate = new DateTime(2023, 5, 31);
        //    order.OrderCode = "#125456";
        //    order.TotalCost = 333;
        //    order.ID = 3;
        //    List<Order> orders = new List<Order>() { order, order, order, order };


        //    if (id == 1)
        //    {

        //        return View("GoShopping");
        //    }
        //    else
        //    {
        //        return View(orders);
        //    }



        //}

        //public IActionResult OrderDetails([FromRoute] int id) // get from link 
        //{
        //    // --> send this order to view .. 
        //    //Order order =  User.getOrder(id , id);

        //    // Testing 
        //    Order order = new Order();
        //    order.OrderDate = new DateTime(2023, 5, 31);
        //    order.OrderCode = " #131321";
        //    order.PaymentMethod = PaymentMethod.CashOnDelivery; // doesn't work 
        //    order.TotalCost = 7000;
        //    // shipping cost total 
        //    Item item = new Item();
        //    item.Name = "Bedroom";


        //    item.Image = "1.jpg";
        //    item.Image2 = "1.jpg";
        //    item.Price = 78;
        //    item.discount = "%15";

        //    item.PriceAfterSale = 48;
        //    Order_Item item2 = new Order_Item();
        //    item2.ID = 2322;
        //    item2.OrderID = id;
        //    item2.item = item;
        //    item2.Quantity = 2;

        //    item2.OrderState = OrderState.Delivering; // problem 
        //    List<Order_Item> items = new List<Order_Item>();
        //    items.Add(item2);
        //    items.Add(item2);
        //    order.Order_Item = items;

        //    // customer testing 

        //    Address address = new Address();
        //    address.description = "Dreams Land , Dreams Schools";
        //    address.phone = "1004443226";
        //    address.FirstName = "Hazem ";
        //    address.LastName = "Abdelstatter";
        //    ViewData["Address"] = address;

        //    return View(order);
        //}
        //public IActionResult Warrently()
        //{
        //    return View();
        //}

        //public IActionResult UpdatePassword(int id)
        //{
        //    //Customer customer = User.GetById(id);	

        //    return View();

        //}
        //public IActionResult Logout()
        //{
        //    return View();
        //}


    }
}




