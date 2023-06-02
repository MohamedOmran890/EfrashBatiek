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
    public class ItemShopController : Controller
    {
        private readonly Context _context;
        IItemRepository itemRepo;
        IBrandRepository brandRepository;
        IShopRepository shopRepository;
        IProductRepository productRepository; 

        public ItemShopController(Context context,IItemRepository itemRepo)
        {
            _context = context;
            this.itemRepo = itemRepo;
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
        public IActionResult Create()
        {
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name");
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName");
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Code,Name,Description,Price,Image,Image2,Brand_ID,QuantityInStore,ShopID,ProductID")] Item item)
        {
            if (ModelState.IsValid)
            {
                itemRepo.Create(item);
                return RedirectToAction("Index");
            }
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            return View(item);
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

        // POST: Item2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
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
