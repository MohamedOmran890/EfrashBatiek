using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EfrashBatek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository _Product;
        IItemRepository _Item;

        public HomeController(ILogger<HomeController> logger, IProductRepository product, IItemRepository item)
        {
            _logger = logger;
            _Product = product;
            _Item = item;
        }

        public IActionResult TrendingProducts()
        {
            //var ans = _Product.GetAll();
            Item item = new Item();
            item.Name = "utrt65u";
           // item.ID = 2;

            item.Image = "Brown right L shaped sofa.jpg";
            item.Code = "hgtg56";
            item.Description = "gfkgfjng";
            item.Image2 = "LF-L000301.jpg";
            item.Price = 78;
            item.discount = "%15";
            item.PriceAfterSale = 48;
            item.ShopID = 1;
            item.Brand_ID = 1;
            item.QuantityInStore = 1;
            item.ProductID = 1;
            //_Item.Create(item);


            Item item2 = new Item();
            item2.Name = "Bedroom";

            item2.Image = "Brown right L shaped sofa.jpg";
            item2.Image2 = "LF-L000301.jpg";
            item2.Price = 78;
            item2.discount = "%15";
            item2.PriceAfterSale = 48;

            List<Item> list = new List<Item>();
            list.Add(item);
            list.Add(item2);




            Product product = new Product() { Items = list };
            Product product2 = new Product() { Items = list };
            List<Product> products = new List<Product>()
            { product , product2 };
            return View(products);
            /*************************/
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