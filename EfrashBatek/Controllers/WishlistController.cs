using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace EfrashBatek.Controllers
{
    public class WishlistController : Controller
    {
        private readonly Context _context;
        IWishListRepository userwishlist;
        ICustomerRepository user;
        IItemRepository item;
        public WishlistController(Context context)
        {
            _context = context;
            //item = new WishListRepository(_context);
        }


        public ActionResult ShowItems()
        {
            List<Item> ItemModel = _context.Items.ToList();
            return View("ShowItems", ItemModel);
        }
        /*public ActionResult AddAllToCart()
        {
            List<Item> ItemModel = _context.Items.ToList();
            return View("ShowItems", ItemModel);
        }*/
    }
}
