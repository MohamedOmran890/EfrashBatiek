using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Hosting;
using EfrashBatek.ViewModel;
using System.IO;

namespace EfrashBatek.Controllers
{
    public class ItemShopController : Controller
    {
        private readonly Context _context;
        IItemRepository itemRepo;
        IBrandRepository brandRepository;
        IShopRepository shopRepository;
        IProductRepository productRepository;
        IWebHostEnvironment Ih;

        public ItemShopController(Context context, IWebHostEnvironment Ih,IItemRepository itemRepo, IBrandRepository brandRepository, IShopRepository shopRepository, IProductRepository productRepository)
        {
            _context = context;
            this.itemRepo = itemRepo;
            this.brandRepository = brandRepository;
            this.shopRepository = shopRepository;
            this.productRepository = productRepository;
            this.Ih = Ih;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(itemRepo.GetAll());
        }

        [HttpGet]
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = itemRepo.GetById((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpGet]
        //1
        public IActionResult Create()
        {
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name");
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName");
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //2
        public IActionResult Create(ItemVM item)
        {
            if (ModelState.IsValid)
            {
                Item itm = new Item();
                itm.Name = item.Name;
                itm.Code = item.Code;
                itm.Price = item.Price;
                itm.Description = item.Description;
                itm.QuantityInStore = item.QuantityInStore;
                //itm.Brand.Name = item.Brand.Name;
                //itm.Product.ProductName = item.Product.ProductName;
                //itm.Shop.Name = item.Shop.Name;
                itm.discount = item.discount;

                string Filename = Guid.NewGuid().ToString() + item.Image.FileName;
                string Filename2 = Guid.NewGuid().ToString() + item.Image2.FileName;
                string Filename3 = Guid.NewGuid().ToString() + item.Image3.FileName;
                string Filename4 = Guid.NewGuid().ToString() + item.Image4.FileName;
                string Filename5 = Guid.NewGuid().ToString() + item.Image5.FileName;
                var fs = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename), FileMode.Create);
                item.Image.CopyTo(fs);
                itm.Image = Filename;

                var fs2 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename2), FileMode.Create);
                item.Image2.CopyTo(fs2);
                itm.Image2 = Filename2;

                var fs3 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename3), FileMode.Create);
                item.Image2.CopyTo(fs3);
                itm.Image3 = Filename3;

                var fs4 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename4), FileMode.Create);
                item.Image4.CopyTo(fs4);
                itm.Image4 = Filename4;

                var fs5 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename5), FileMode.Create);
                item.Image5.CopyTo(fs5);
                itm.Image5 = Filename5;


                int check = itemRepo.Update((int)id, itm);
                return RedirectToAction("Index");

                itemRepo.Create(itm);
                return RedirectToAction("Index");
            }
            //ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            //ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            //ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            //return View(item);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = itemRepo.GetById((int)id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            return View(item);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Code,Name,Description,Price,Image,Image2,discount,PriceAfterSale,Brand_ID,QuantityInStore,ShopID,ProductID")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                   int check=itemRepo.Update((int)id,item);
                return RedirectToAction("Index");
            }
            //ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            //ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            //ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            return View(item);
        }

        // GET: Item2/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = itemRepo.Delete((int)id);
            if (item == 0)
            {
                return NotFound();
            }

            return View(item);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public  IActionResult DeleteConfirmed(int id)
        //{
        //    var item =  _context.Items.Find(id);
        //    _context.Items.Remove(item);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ItemExists(int id)
        //{
        //    return _context.Items.Any(e => e.ID == id);
        //}
    }
}
