using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using EfrashBatek.service;

namespace EfrashBatek.Controllers
{
    public class ProductController : Controller
    {
        private readonly Context _context;
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(Context context,IItemRepository itemRepository,IProductRepository productRepository)
        {
            _context = context;
            _itemRepository = itemRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            //List<ProductName> productName = new List<ProductName>
            //{
            //    ProductName.LivingRoom,
            //    ProductName.YouthAndKidsBedRooms,
            //    ProductName.DiningRoom,
            //    ProductName.LargeAppliances,
            //    ProductName.KitchenAppliances,
            //    ProductName.HomeAppliances,
            //    ProductName.Cookware,
            //    ProductName.Drinkware,
            //    ProductName.Dinnerware,
            //    ProductName.Alometal,
            //    ProductName.Wood,
            //    ProductName.Acrylic
            //};
           // ViewBag.ProductName = productName;

            List<Category> category = new List<Category>
            { 
            Category.furniture,
            Category.Kitchen_utensils,
            Category.Home_Appliances,
            Category.Kitchen_utensils
         
            };
            ViewBag.category=category;
            var all_item=_itemRepository.GetAll();
            return View(all_item);
        }

        // GET: Product/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _itemRepository.GetById((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        public IActionResult ProductByName(ProductName productName)
        {
            var selectByproduct =_productRepository.GetByProduct(productName);
            return View(selectByproduct);
        }
        public IActionResult ItembyCategory(Category category)
        {
            var ans = _productRepository.GetByCategory(category);
            return View(ans);
        }
      
    }
}
