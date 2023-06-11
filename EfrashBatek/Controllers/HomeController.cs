using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace EfrashBatek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository _Product;
        IItemRepository _Item;
		private readonly Context context;

		public HomeController(ILogger<HomeController> logger, IProductRepository product, IItemRepository item , Context context)
        {
            _logger = logger;
            _Product = product;
            _Item = item;
			this.context = context;
		}

        public IActionResult TrendingProducts()
        {

            var list = context.Items // include product inside  to void null expection 
                         .SelectMany(i => context.Order_Items.Where(oi => oi.ItemID == i.ID), (i, oi) => new { Item = i, Order_Item = oi  , productItem =  i.Product })
                         .ToList()
                         .GroupBy(ti => ti.Item, ti => ti.Order_Item)
                         .Select(g => new { product = g.Key, numberOfOrders = g.Count() , productItem = g.Key.Product })
                         .OrderByDescending(x => x.numberOfOrders)
                         .Take(3)
                         .ToList();

            List<Item> items = new List<Item>();
           foreach (var item in list)
            {
                items.Add(item.product);
                
            }
            
            return View(items);    


			
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // contact us
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}