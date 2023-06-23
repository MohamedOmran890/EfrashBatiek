using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using EfrashBatek.service;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;
using System.Security.Policy;

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
      
         
            };
            ViewBag.category=category;
            Dictionary<Category, List<Product>> categoryToProducts = new Dictionary<Category, List<Product>>();
            foreach(var cate in category)
            {
                List<Product> products = _context.Products.Where(i => i.Category == cate).ToList();
               categoryToProducts.Add(cate,products);  
                 

            }
            var all_item=_itemRepository.GetAll();
            return View(all_item);
        }
        public IActionResult Index1(string SearchString)
        {

            List<Category> category = new List<Category>
            {
            Category.furniture,
            Category.Kitchen_utensils,
            Category.Home_Appliances,
               Category.All_Products
            };
           
            Dictionary<Category, List<Product>> categoryToProducts = new Dictionary<Category, List<Product>>();
            foreach (var cate in category)
            {
                List<Product> products = _context.Products.Where(i => i.Category == cate).ToList();
                if (!categoryToProducts.ContainsKey(cate))
                {

                    categoryToProducts.Add(cate, products);

                }


            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                var itm = _context.Items.Where(x => x.Name.Contains(SearchString)).ToList();
                return View(itm);
            }

            ViewBag.categoryToProducts = categoryToProducts;
            var all_item = _itemRepository.GetAll();
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
        public IActionResult ItembyCategory(Category productName)
        {
           
            List<Category> categoryy = new List<Category>
            {
            Category.furniture,
            Category.Kitchen_utensils,
            Category.Home_Appliances,
            Category.All_Products
             


            };

            Dictionary<Category, List<Product>> categoryToProducts = new Dictionary<Category, List<Product>>();
            foreach (var cate in categoryy)
            {
                List<Product> productss = _context.Products.Where(i => i.Category == cate).ToList();
                if (!categoryToProducts.ContainsKey(cate))
                {

                    categoryToProducts.Add(cate, productss);

                }


            }
           
            ViewBag.categoryToProducts = categoryToProducts;
            if (productName == Category.All_Products)
            {
                var all_item = _itemRepository.GetAll();

                return View("Index1", all_item);

            }
            List<Product> products = _context.Products.Where(i => i.Category == productName).ToList();
            List<Item> res = new List<Item>();  
            foreach (var product in products) {
                List<Item> ans =_context.Items.Where(i=>i.ProductID == product.ID).ToList();    
                foreach (var item in ans) { 
                    res.Add(item);  
                
                }



            }


            return View("Index1" ,res);
        }
      
    }
}
