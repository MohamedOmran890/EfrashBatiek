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
        public CartController(IItemRepository repository)
        {
            _itemRepository = repository;
        }
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
                Cart_Item itm = new Cart_Item
                {
                    ItemID = item.ID,
                    Quantity = 1,
                    ItemName= item.Name,
                };
                items.Add(itm);
                HttpContext.Session.Set("cart", items); 
            }
            return RedirectToAction("TrendingProducts","Home");
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
            return RedirectToAction("Cart");

        }
    }
}
