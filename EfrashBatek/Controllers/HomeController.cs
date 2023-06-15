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
           


           var pair = new KeyValuePair<List<Item>,List<Item>> (_Item.NewArrivals(), _Item.Trending());
             
                 
             return View(pair);    


			
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