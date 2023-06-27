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
using Castle.Core.Resource;
using System.Xml.Schema;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace EfrashBatek.Controllers
{
    [Authorize(Roles ="Seller,Admin,Shop")]
    public class ItemShopController : Controller
    {
        private readonly IAddressRepository address;
        private readonly Context _context;
        IItemRepository itemRepo;
        IBrandRepository brandRepository;
        IShopRepository shopRepository;
        IProductRepository productRepository;
        IWebHostEnvironment Ih;
        IIdentityRepository IdentityRepository;
        IStaffRepository staffRepository;
        IOrder_ItemRepository order_ItemRepository;

        public ItemShopController( IAddressRepository address ,  Context context, IWebHostEnvironment Ih, IItemRepository itemRepo, IBrandRepository brandRepository, IShopRepository shopRepository, 
            IProductRepository productRepository,
            IIdentityRepository identityRepository,
            IStaffRepository staffRepository,IOrder_ItemRepository order_ItemRepository)
        {
            this.address = address;
            _context = context;
            this.itemRepo = itemRepo;
            this.brandRepository = brandRepository;
            this.shopRepository = shopRepository;
            this.productRepository = productRepository;
            this.Ih = Ih;
            IdentityRepository = identityRepository;
            this.staffRepository = staffRepository;
            this.order_ItemRepository = order_ItemRepository;

        }
        //  show all products of shop  
		public IActionResult ShopItem(string SearchString)
		{
			var user = IdentityRepository.GetUser();
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var Seller = staffRepository.GetByUser(user.Id);
			if (Seller == null)
			{
				return RedirectToAction("Login", "Account");
			}
			var shop = shopRepository.GetById(Seller.ShopID);
			var itm = shopRepository.ItemByShop(shop.ID);
            foreach (var item in itm)
            {
                item.Product = _context.Products.FirstOrDefault(i => i.ID == item.ProductID);
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                var item = _context.Items.Where(x => x.Name.Contains(SearchString)).ToList();
                return View(item);
            }

            return View(itm);
		}


        
        [HttpGet]
        public IActionResult Seller()
        {

            // user- -> selller id --> user --> shopid --> orderitem
            User user = IdentityRepository.GetUser();
            Staff seller = _context.The_Staff.FirstOrDefault(i => i.UserId == user.Id);
            var orders = _context.Order_Items.Where(i => i.ShopID == seller.ShopID).ToList();
            int totalCustomers = 0;
            List<Customer> cus = _context.Customers.Where(i => true).ToList();  
            foreach(var customer in cus)
            {
                var orderss = _context.Orders.Where(i=>i.CustomerID == customer.Id);
                bool found = false;
                foreach(var order in orderss)
                {
                    if(order.Order_Item!=null)
                    {
                        foreach (var item in order.Order_Item)
                        {
                            if (item.ShopID == seller.ShopID)
                            {
                                found = true; break;
                            }

                        }

                    }    
                    
                    if (found) break;
                }
                if (found) totalCustomers++;
                
            }
            var Dash = new DashboardViewModel
            {

                TotalOrders = orders.Count(),
                TotalCustomers = totalCustomers,    

            };


            return View(Dash);
        }
      
      
        
        [HttpGet]
        // show details of item 
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
        // create item
       
        public IActionResult Create()
        {
			var HaveSession = HttpContext.Session.GetString("Id");
			if (HaveSession == null)
			{
				return RedirectToAction("Login", "Account");
			}
			ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Category");
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName");
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //save item
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
                return RedirectToAction("ShopItem");
            }
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            return View(item);
        }
         // edit item
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
        // save edit 
        public IActionResult Edit(ItemVM item)
        {
            var ItmeEdit = itemRepo.GetById(item.ID)
;            if (ItmeEdit.ID != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ItmeEdit.Name = item.Name;
                ItmeEdit.Code = item.Code;
                ItmeEdit.Price = item.Price;
                ItmeEdit.Description = item.Description;
                ItmeEdit.QuantityInStore = item.QuantityInStore;
               ItmeEdit.discount = item.discount;
                ItmeEdit.PriceAfterSale = item.PriceAfterSale;
                ItmeEdit.Brand_ID = item.Brand_ID;
                ItmeEdit.ShopID = item.ShopID;
                ItmeEdit.ProductID = item.ProductID;


                if (item.Image != null && item.Image.Length > 0)
                {
                    string Filename = Guid.NewGuid().ToString() + item.Image.FileName;
                    var fs = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename), FileMode.Create);
                    item.Image.CopyTo(fs);
                    ItmeEdit.Image = Filename;
                }
                if (item.Image2 != null && item.Image2.Length > 0)
                {
                    string Filename2 = Guid.NewGuid().ToString() + item.Image2.FileName;
                    var fs2 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename2), FileMode.Create);
                    item.Image2.CopyTo(fs2);
                    ItmeEdit.Image2 = Filename2;
                }
                if (item.Image3 != null && item.Image3.Length > 0)
                {
                    string Filename3 = Guid.NewGuid().ToString() + item.Image3.FileName;
                    var fs3 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename3), FileMode.Create);
                    item.Image2.CopyTo(fs3);
                    ItmeEdit.Image3 = Filename3;
                }

                if (item.Image4 != null && item.Image4.Length > 0)
                {
                    string Filename4 = Guid.NewGuid().ToString() + item.Image4.FileName;
                    var fs4 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename4), FileMode.Create);
                    item.Image4.CopyTo(fs4);
                    ItmeEdit.Image4 = Filename4;
                }
                if (item.Image5 != null && item.Image5.Length > 0)
                {
                    string Filename5 = Guid.NewGuid().ToString() + item.Image5.FileName;
                    var fs5 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename5), FileMode.Create);
                    item.Image5.CopyTo(fs5);
                    ItmeEdit.Image5 = Filename5;
                }

                int check = itemRepo.Update(ItmeEdit);
                return RedirectToAction(nameof(ShopItem));
            }
            ViewData["BrandName"] = new SelectList(brandRepository.GetAll(), "ID", "Name", item.Brand_ID);
            ViewData["ProductName"] = new SelectList(productRepository.GetAll(), "ID", "ProductName", item.ProductID);
            ViewData["ShopName"] = new SelectList(shopRepository.GetAll(), "ID", "Name", item.ShopID);
            return View(item);
        }

        // GET: Item2/Delete/5
        // delete item
        public IActionResult Delete(int id)
        {
            var item = _context.Items.FirstOrDefault(i => i.ID == id);
            foreach(var item2 in _context.Order_Items)
            {
                if(item2.ItemID == id)
                {
                    _context.Remove(item2); 

                }
            }
            foreach(var item2  in _context.WishListItems)
            {

                 if(item2.ItemId == id) { 
                
                _context.Remove(item2); }
            }
            _context.Items.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("ShopItem");  
        }



        // List<Order Items> ??
        public IActionResult OrderShop(string SearchString)
        {
            var user = IdentityRepository.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var Seller = staffRepository.GetByUser(user.Id);
            if (Seller == null)
            {
                return View(new List<Order_Item>());
            }
            var shop = shopRepository.GetById(Seller.ShopID);
       
            List<OrderVm> ShopOrder = new List<OrderVm>();
           
           var orders =  _context.Orders.ToList();
            foreach (var order in orders) {
                order.Order_Item = _context.Order_Items.Where(i => i.OrderID == order.ID).ToList(); 

                var list = order.Order_Item.Where(i => i.ShopID == shop.ID).ToList ();
            
                if(list.Count() >0) {
                    
                    order.Customer = _context.Customers.FirstOrDefault(i => i.Id == order.CustomerID);
                    order.Customer.User = _context.Users.FirstOrDefault(i => i.Id == order.Customer.UserId);
                    int total = 0;
                    OrderState orderState = OrderState.Delivering; 
                    foreach (var item in list)
                    {
                         orderState= item.OrderState;   
                        item.item = _context.Items.FirstOrDefault(i => i.ID == item.ItemID);
                        total += item.Quantity * (int)item.item.Price;


                    }
                     
                    Order NewObject = new Order();
                    NewObject.TotalCost = total;
                    NewObject.OrderCode =order.OrderCode;   
                    NewObject.OrderDate = order.OrderDate;
                    Customer customer = new Customer();
                    customer.User = order.Customer.User;
                    customer = order.Customer;
                    NewObject.Customer = customer;
                    NewObject.ID =order.ID;
                    OrderVm vm = new OrderVm();
                    vm.order = NewObject;
                    vm.state = orderState;
                    ShopOrder.Add(vm);
                }
               
                
                
               
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
             var itm = _context.Items.Where(x => x.Name.Contains(SearchString)).ToList();           
                return View(itm);
            }

            return View(ShopOrder);
           

        }
        // delete  order 
        public IActionResult DeleteOrder(int id)
        {
            order_ItemRepository.Delete(id);
            return RedirectToAction("OrderShop","ItemShop");

        }

        [Authorize(Roles = "Seller,Shop,Admin")]
        public IActionResult CustomOrder()
        {
            var custom = _context.Customs.ToList();
            return View(custom);
        }
        public IActionResult OrderDetails(int id)
        {
            Order order = _context.Orders.FirstOrDefault(i => i.ID == id);
            var list = _context.Order_Items.Where(i => i.OrderID == order.ID).ToList();
            var addresss = address.GetbyID(order.AddressID);
            order.Address = addresss;
            var user = IdentityRepository.GetUser();
            
            var Seller = staffRepository.GetByUser(user.Id);
            List<Order_Item> items = new List<Order_Item>();
            var shop = shopRepository.GetById(Seller.ShopID);
            foreach (var item in list)
            {
                item.item = _context.Items.FirstOrDefault(i => i.ID == item.ItemID);
                if(item.ShopID == shop.ID)
                items.Add(item);
            }
            order.Order_Item = list;

            ViewBag.Items = items; 
            return View(order);
        }
        public IActionResult CustomDetails(int id)
        {
            var model = _context.Customs.FirstOrDefault(i => i.ID == id);

            return View( model);
        }

    }
}
