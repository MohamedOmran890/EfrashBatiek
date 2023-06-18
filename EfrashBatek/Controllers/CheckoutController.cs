
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class CheckoutController : Controller
    {
        IAddressRepository addressRepository;
    private readonly ICartRepository cart;
    private readonly Context context;
    IIdentityRepository _identityRepository;
        public CheckoutController(IAddressRepository Address,IIdentityRepository identityRepository , IAddressRepository addressRepository ,ICartRepository cart , Context context )
        {
            addressRepository = Address;
          _identityRepository = identityRepository;
        this.context = context;
        this.addressRepository = addressRepository;
        this.cart = cart;
        this.context = context;
    }
   
    public ActionResult defaultaddress(int cartID ) { 

        var items = cart.LoadFromCookie();
        var list = items.Where(i=>i.CartID == cartID).ToList();
              foreach ( var item in list ) {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
        
        }
        ViewBag.list = list;   
        ViewBag.cartID = cartID;    
               return View(addressRepository.View());   
    }
	
	public IActionResult ViewAddressDetails(int id ) 
	{
        
		var Address = addressRepository .GetbyID(id);

		return View("EditAddress", Address);

	}
    
	[HttpPost]
	public IActionResult EditAddress(Address New) 
	{
		addressRepository.Edit(New);

		return RedirectToAction("defaulrtaddress");

	}
    [HttpPost]
	public IActionResult PaymentMethod(int cartID , int selectedAddressId)
	{
        ViewBag.cartID = cartID;
       
        ViewBag.selectedAddressId = selectedAddressId;  
        return View();
	}
    public IActionResult Confirmation(int cartID , int selectedAddressId)
    {
        
   
		return RedirectToAction("ViewOrders", "MyProfile", new { @cartID=cartID , @selectedAddressId = selectedAddressId , @Isorder = true});
	}

  

    //public IActionResult Address() //old1
    //{
    //    var zone = new SelectList(Enum.GetValues(typeof(Zone)));
    //    ViewData["Zone"] = zone;
    //    return View("Address1");
    //}
    public IActionResult Addresss()//done1
    {
        var zone = new SelectList(Enum.GetValues(typeof(Zone)));
        ViewData["Zone"] = zone;
        return View("Address1");
    }
    //public IActionResult SaveAddress(Address obj)
    //    {

    //        addressRepository.Create(obj);
    //        return RedirectToAction("PaymentMethod");
    //        /********/
    //    }
    public IActionResult SaveAddress(Address obj)
    {

        addressRepository.Create(obj);
        return RedirectToAction("PaymentMethod");
        
    }
    public IActionResult Index1()
    {
        return View();
    }
    
       

}