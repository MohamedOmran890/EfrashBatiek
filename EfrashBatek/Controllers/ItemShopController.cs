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
        IIdentityRepository IdentityRepository;
        IStaffRepository staffRepository;

        public ItemShopController(Context context, IWebHostEnvironment Ih, IItemRepository itemRepo, IBrandRepository brandRepository, IShopRepository shopRepository, 
            IProductRepository productRepository,IIdentityRepository identityRepository,IStaffRepository staffRepository)
        {
            _context = context;
            this.itemRepo = itemRepo;
            this.brandRepository = brandRepository;
            this.shopRepository = shopRepository;
            this.productRepository = productRepository;
            this.Ih = Ih;
            IdentityRepository = identityRepository;
            this.staffRepository = staffRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(itemRepo.GetAll());
        }
        //bymee
        [HttpGet]
        public IActionResult Seller()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        //test by mee
        public IActionResult ShopItems()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id)
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
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Category");
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName");
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //2
        public async Task<IActionResult> Create(ItemVM item)
        {
            if (ModelState.IsValid)
            {

                Item itm = new Item();
                itm.Name = item.Name;
                itm.Code = item.Code;
                itm.Price = item.Price;
                itm.Description = item.Description;
                itm.QuantityInStore = item.QuantityInStore;
                itm.discount = item.discount;
                itm.PriceAfterSale = item.PriceAfterSale;
                itm.Brand_ID = item.Brand_ID;
                itm.ShopID = item.ShopID;
                itm.ProductID = item.ProductID;
                itm.DateTime = item.DateTime;
                if (item.Image != null && item.Image.Length > 0)
                {
                    string Filename = Guid.NewGuid().ToString() + item.Image.FileName;
                    var fs = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename), FileMode.Create);
                    item.Image.CopyTo(fs);
                    itm.Image = Filename;
                }
                if (item.Image2 != null && item.Image2.Length > 0)
                {
                    string Filename2 = Guid.NewGuid().ToString() + item.Image2.FileName;
                    var fs2 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename2), FileMode.Create);
                    item.Image2.CopyTo(fs2);
                    itm.Image2 = Filename2;
                }
                if (item.Image3 != null && item.Image3.Length > 0)
                {
                    string Filename3 = Guid.NewGuid().ToString() + item.Image3.FileName;
                    var fs3 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename3), FileMode.Create);
                    item.Image2.CopyTo(fs3);
                    itm.Image3 = Filename3;
                }

                if (item.Image4 != null && item.Image4.Length > 0)
                {
                    string Filename4 = Guid.NewGuid().ToString() + item.Image4.FileName;
                    var fs4 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename4), FileMode.Create);
                    item.Image4.CopyTo(fs4);
                    itm.Image4 = Filename4;
                }
                if (item.Image5 != null && item.Image5.Length > 0)
                {
                    string Filename5 = Guid.NewGuid().ToString() + item.Image5.FileName;
                    var fs5 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename5), FileMode.Create);
                    item.Image5.CopyTo(fs5);
                    itm.Image5 = Filename5;
                }



                itemRepo.Create(itm);
                await _context.SaveChangesAsync();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ItemVM item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

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
                itm.PriceAfterSale = item.PriceAfterSale;
                itm.Brand_ID = item.Brand_ID;
                itm.ShopID = item.ShopID;
                itm.ProductID = item.ProductID;


                if (item.Image != null && item.Image.Length > 0)
                {
                    string Filename = Guid.NewGuid().ToString() + item.Image.FileName;
                    var fs = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename), FileMode.Create);
                    item.Image.CopyTo(fs);
                    itm.Image = Filename;
                }
                if (item.Image2 != null && item.Image2.Length > 0)
                {
                    string Filename2 = Guid.NewGuid().ToString() + item.Image2.FileName;
                    var fs2 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename2), FileMode.Create);
                    item.Image2.CopyTo(fs2);
                    itm.Image2 = Filename2;
                }
                if (item.Image3 != null && item.Image3.Length > 0)
                {
                    string Filename3 = Guid.NewGuid().ToString() + item.Image3.FileName;
                    var fs3 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename3), FileMode.Create);
                    item.Image2.CopyTo(fs3);
                    itm.Image3 = Filename3;
                }

                if (item.Image4 != null && item.Image4.Length > 0)
                {
                    string Filename4 = Guid.NewGuid().ToString() + item.Image4.FileName;
                    var fs4 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename4), FileMode.Create);
                    item.Image4.CopyTo(fs4);
                    itm.Image4 = Filename4;
                }
                if (item.Image5 != null && item.Image5.Length > 0)
                {
                    string Filename5 = Guid.NewGuid().ToString() + item.Image5.FileName;
                    var fs5 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename5), FileMode.Create);
                    item.Image5.CopyTo(fs5);
                    itm.Image5 = Filename5;
                }

                int check = itemRepo.Update((int)id, itm);
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
        public IActionResult ShopItem()
        {
            var user = IdentityRepository.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login","Account");
            }
            var Seller= staffRepository.GetByUser(user.Id);
            var shop = shopRepository.GetById(Seller.ShopID);
               var itm= shopRepository.ItemByShop(shop.ID);
            return View(itm);
        }
		

		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmedAsync(int id)
		//{
		//    var item = _context.Items.Find(id);
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
