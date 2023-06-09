﻿
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

[Authorize]
public class CheckoutController : Controller
    {
        IAddressRepository addressRepository;
    private readonly ICartRepository cart;
    private readonly Context context;
    private readonly IOrder_ItemRepository order_Item;
    private readonly IOrderRepository orderRepository;
    private readonly ICustomerRepository customerRepository;
    IIdentityRepository _identityRepository;
        public CheckoutController( IOrder_ItemRepository order_Item , IOrderRepository orderRepository,   ICustomerRepository customerRepository  , IAddressRepository Address,IIdentityRepository identityRepository , IAddressRepository addressRepository ,ICartRepository cart , Context context )
        {
            addressRepository = Address;
        this.order_Item = order_Item;
        this.orderRepository = orderRepository;
        this.customerRepository = customerRepository;
        _identityRepository = identityRepository;
        this.context = context;
        this.addressRepository = addressRepository;
        this.cart = cart;
        this.context = context;
    }
   
    // done test 
    public ActionResult defaultaddress(int cartID) { 

        var items = cart.LoadFromCookie();
        var list = items.Where(i=>i.CartID == cartID).ToList();
        int total = 0;
              foreach ( var item in list ) {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            total += item.Quantity * (int)item.Item.Price;
        
        }
        ViewBag.list = list;
        @ViewBag.price = total;
        ViewBag.cartID = cartID;    
               return View("ChooseAddress" , addressRepository.View());   
    }
 
    // done test 
	public ActionResult CreateAddress(int cartid )
    {
        var items = cart.LoadFromCookie();
        var list = items.Where(i => i.CartID == cartid).ToList();
       List<Order_Item> ordersitems= new List<Order_Item>();    
		foreach (var item in list)
        {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            Order_Item order_item = new Order_Item();
            order_item.item = item.Item; 
            ordersitems.Add(order_item);



		}
        ViewBag.list = list;
        ViewBag.cartID = cartid;
        Order order = new Order();
        order.Order_Item = ordersitems;
        ViewBag.order = order;  
       
        return View();  
    }
    [HttpPost]
    public ActionResult CreateAddress(Address address , int cartid)
    {
        addressRepository.Create(address);

		return RedirectToAction("defaultAddress", new { cartID = cartid });


	}

	public IActionResult ViewAddressDetails(int id , int cartid) 
	{
        
		var Address = addressRepository .GetbyID(id);
		var items = cart.LoadFromCookie();
		var list = items.Where(i => i.CartID == cartid).ToList();
        int totalcost = 0;  
		foreach (var item in list)
		{
			item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            totalcost += item.Quantity * (int)item.Item.Price;

		}
		ViewBag.list = list;
        ViewBag.price = totalcost;
		ViewBag.cartID = cartid;
		return View("Checkout", Address);

	}
    
	[HttpPost]
	public IActionResult EditAddress(Address New) 
	{
		addressRepository.Edit(New);

		return RedirectToAction("defaultaddress");

	}
    [HttpPost]
	public IActionResult PaymentMethod(int cartID , int selectedAddressId)
    {
        if (selectedAddressId == 0)
        {
            return RedirectToAction("CreateAddress" , cartID);
        }
        var items = cart.LoadFromCookie();
        var list = items.Where(i => i.CartID == cartID).ToList();
        int total = 0;
        foreach (var item in list)
        {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            total += (int)item.Item.Price * item.Quantity;

        }
        ViewBag.list = list;
        ViewBag.cartID = cartID;
        ViewBag.price = total;

        ViewBag.selectedAddressId = selectedAddressId;  
        return View("Payment" , total);
	}
    public IActionResult Confirmation(int cartID , int selectedAddressId)
    {
        List<Order> orders = new List<Order>();
        Customer customer = customerRepository.GetCustomerbyUserId();
        
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
            order_Item.Shop = item.Item.Shop;
            order_Item.ShopID = item.Item.ShopID;   
                order_Items.Add(order_Item);
            }




            order.TotalCost = totalcost;
            DateTime orderDate = DateTime.Now;

            order.OrderDate = orderDate.Date;
        order.PaymentMethod = EfrashBatek.Models.PaymentMethod.CashOnDelivery;
       
 

        order.AddressID = selectedAddressId;
            order.Address = addressRepository.GetbyID(selectedAddressId);

            order.CustomerID = customer.Id;

            orderRepository.Create(order);
            Order findorder = context.Orders.FirstOrDefault(i => i.OrderCode == code);
            foreach (var item in order_Items)
            {
                item.OrderID = findorder.ID;

            order_Item.Create(item);

            }

            findorder.Order_Item = order_Items;

            context.Orders.Update(findorder);
            context.SaveChanges();

            cart.Clear();
        return View();
        



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
    
       public ActionResult Details (int id , int address ) {

		var items = cart.LoadFromCookie();
		var list = items.Where(i => i.CartID == id).ToList();
        List<Order_Item> itemss = new List<Order_Item>();       
		foreach (var item in list)
		{
			item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
            Order_Item orderitem = new Order_Item();
            orderitem.item = item.Item;
            itemss.Add(orderitem);
		}
        Order order  = new Order();
        order.Order_Item = itemss;
        order.Address = context.Addresses.FirstOrDefault(i => i.ID == address);
       


		return View ("OrderDetails", order );
	}

}