
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
    public ActionResult defaultaddress(int cartID ) { 

        var items = cart.LoadFromCookie();
        var list = items.Where(i=>i.CartID == cartID).ToList();
              foreach ( var item in list ) {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
        
        }
        ViewBag.list = list;   
        ViewBag.cartID = cartID;    
               return View("ChooseAddress" , addressRepository.View());   
    }
 
    // done test 
	public ActionResult CreateAddress(int cartid )
    {
        var items = cart.LoadFromCookie();
        var list = items.Where(i => i.CartID == cartid).ToList();
        foreach (var item in list)
        {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);

        }
        ViewBag.list = list;
        ViewBag.cartID = cartid;
        return View();  
    }
    [HttpPost]
    public ActionResult CreateAddress(Address address)
    {
        addressRepository.Create(address);

        return RedirectToAction("defaultAddress" ,"Checkout");
       
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
        var items = cart.LoadFromCookie();
        var list = items.Where(i => i.CartID == cartID).ToList();
        foreach (var item in list)
        {
            item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);

        }
        ViewBag.list = list;
        ViewBag.cartID = cartID;

        ViewBag.selectedAddressId = selectedAddressId;  
        return View("Payment");
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
                order_Item.Shop = context.Shops.FirstOrDefault();
                order_Items.Add(order_Item);
            }




            order.TotalCost = totalcost;
            DateTime orderDate = DateTime.Now;

            order.OrderDate = orderDate.Date;
       
 

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
    
       

}