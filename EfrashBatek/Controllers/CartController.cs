using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.Controllers
{
    public class CartController : Controller
    {
        IItemRepository _itemRepository;
        IIdentityRepository _identityRepository;
        public CartController(IItemRepository repository, IIdentityRepository identityRepository )
        {
            _itemRepository = repository;
            _identityRepository = identityRepository;
        }

        public IActionResult Index() {
            List<Cart_Item> items = HttpContext.Session.Get<List<Cart_Item>>("cart");
            if (items == null)
            {
                // Initialize the list if it is null
                items = new List<Cart_Item>();
            }
            return View(items);

        }
        [HttpPost]
        public IActionResult AddToCart(int Id)
        {
            var HaveSession = HttpContext.Session.GetString("Id");
            List<Cart_Item> items = HttpContext.Session.Get<List<Cart_Item>>("cart");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (items == null)
            {
                // Initialize the list if it is null
                items = new List<Cart_Item>();
            }
            Cart_Item x = items.FirstOrDefault(i => i.ItemID == Id);
            if(x!=null)
            {
                x.Quantity += 1;//Increment 1 if Exists in cart
            }
            else
            { 
            Item item = _itemRepository.GetById(Id);
                if(item==null)
                {
                    return Content("The Product Not Found");
                }
                // alyaa changes to avoid  ReferenceLoopHandling cycle  error 
                var itemdata = new itemData();

                itemdata.price = (int)item.Price;
                itemdata.image = item.Image;    
                itemdata.Name = item.Name;
                // id of item with itemdata 
                itemdata.itemid = item.ID;

                Cart_Item itm = new Cart_Item
                {
                    ItemID = item.ID,
                    Quantity = 10,
                    itemData = itemdata,
                    ItemName= item.Name,
                };
               
              
                items.Add(itm);
                 HttpContext.Session.Set("cart", items); 
            }
            return RedirectToAction("Index","Cart");
        }
        public IActionResult Cart()
        {
            List<Cart_Item> items = HttpContext.Session.Get<List<Cart_Item>>("cart");
            if (items == null)
            {
                // Initialize the list if it is null
                items = new List<Cart_Item>();
            }
            return View(items);
        }
        public IActionResult RemoveFromCart(int Id)
        {
            List<Cart_Item> items = HttpContext.Session.Get<List<Cart_Item>>("cart");
            if (items == null)
            {
                // Initialize the list if it is null
                items = new List<Cart_Item>();
            }
            Cart_Item itm = items.FirstOrDefault(i => i.ItemID == Id);
            if (itm != null)
                items.Remove(itm);
            HttpContext.Session.Set("cart", items);
            return RedirectToAction("Index" , "Cart");

        }

        [HttpPost]
        public ActionResult UpdateQuantity(int itemId, int newQuantity)
        {
            List<Cart_Item> items = HttpContext.Session.Get<List<Cart_Item>>("cart");
            Cart_Item x = items.FirstOrDefault(i => i.ItemID == itemId);
          

            if (x != null)
            {
                x.Quantity = newQuantity;
                
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Item not found" });
            }
        }
    }
}
