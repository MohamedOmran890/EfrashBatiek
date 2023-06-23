using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IContact_UsRepository _ContactUs;

        public HomeController(ILogger<HomeController> logger, IProductRepository product,
            IItemRepository item, Context context, IContact_UsRepository contact_Us)
        {
            _logger = logger;
            _Product = product;
            _Item = item;
            this.context = context;
            _ContactUs = contact_Us;
        }

        public IActionResult TrendingProducts(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var itm = context.Items.Where(x => x.Name.Contains(SearchString)).ToList();
                return View(itm);
            }

            var pair = new KeyValuePair<List<Item>, List<Item>>(_Item.NewArrivals(), _Item.Trending());
            return View(pair);

        }
        public IActionResult Privacy()
        {
            return View();
        }
        // contact us
        public IActionResult ContactUs()
        {
            return View("ContactUs1");
        }
        [HttpPost]
        public IActionResult ContactUs(Contact_Us contact)
        {
            if (contact != null && ModelState.IsValid)
            {
                _ContactUs.Create(contact);
                return RedirectToAction("Success");
            }
            else
            {
                return View("failedpage");
            }


        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Unauthorized()
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