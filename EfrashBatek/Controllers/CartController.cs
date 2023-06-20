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
        private readonly ICartRepository cartRepository;
        private readonly Context context;
        private readonly IIdentityRepository _identityRepository;

        public CartController(IItemRepository repository, ICartRepository cartRepository, Context context)
        {
            _itemRepository = repository;
            this.cartRepository = cartRepository;
            this.context = context;
        }
        public IActionResult Index()
        {

            var HaveSession = HttpContext.Session.GetString("Id");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var items = cartRepository.LoadFromCookie();

            int totalPrice = 0;
            foreach (var item in items)
            {
                item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemID);
                if(item.Item != null) {
                    totalPrice += item.Quantity * (int)item.Item.Price;
                }
                ViewBag.cartID = item.CartID;

            }
            ViewBag.TotalPrice = totalPrice;

           
          
            if(items.Count == 0)
            {
                return View("empty");
            }
            return View(items);
        }

        public IActionResult AddToCart(int id )
        {
            var HaveSession = HttpContext.Session.GetString("Id");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }

            cartRepository.AddToCart(id);
            return RedirectToAction("Index");


        }

        public IActionResult Remove(int itemID)
        {
            cartRepository.RemoveItem(itemID);


            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult updateQuantity(int itemID, int newQuantity)
        {

            cartRepository.UpdateItemQuantity(itemID, newQuantity);

            return RedirectToAction("Index");

        }

    }
}
