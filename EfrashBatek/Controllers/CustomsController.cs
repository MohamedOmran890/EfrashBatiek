    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using Microsoft.AspNetCore.Identity;
using EfrashBatek.service;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EfrashBatek.ViewModel;

namespace EfrashBatek.Controllers
{
    public class CustomsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        public readonly ICustomRepository _customRepository;
        private readonly IWebHostEnvironment Ih;
        IIdentityRepository _identityRepository;
        IWishListRepository _wishListRepository;
        public CustomsController(Context context,UserManager<User> userManager,
            ICustomRepository customRepository, IWebHostEnvironment webHostEnvironment,
            IIdentityRepository identityRepository,IWishListRepository wishListRepository)
        {
            _context = context;
            _userManager = userManager;
            _customRepository = customRepository;
            Ih = webHostEnvironment;
            _identityRepository = identityRepository;
            _wishListRepository = wishListRepository;
        }

        // GET: Customs
        public async Task<IActionResult> Index()
        {

            var userid = _identityRepository.GetUserID();
            if(userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var data = _customRepository.GetByCustomer(userid);
            return View(data);
        }

        // GET: Customs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custom = await _context.Customs
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (custom == null)
            {
                return NotFound();
            }

            return View(custom);
        }

        public IActionResult Create()
        {
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return RedirectToAction("Login","Account");
            //}
            var zone= new SelectList(Enum.GetValues(typeof(Zones)));
            ViewData["Zone"] = zone;
            return View();
        }
        public IActionResult Create2()
        {
            var zone = new SelectList(Enum.GetValues(typeof(Zones)));
            ViewData["Zone"] = zone;
            return View();
        }

          [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomVM custom)
        {
            if (ModelState.IsValid)
            {
                var userId = _identityRepository.GetUserID();
                var customer = _wishListRepository.GetCustomerWithUser(userId);
                string Filename = Guid.NewGuid().ToString() + custom.Image.FileName;
                Custom cus = new Custom();
                cus.Name = custom.Name;
                cus.Phone = custom.Phone;
                cus.Description = custom.Description;
                cus.Zone = custom.Zones;//(Zone)custom.Zones;
                var fs = new FileStream(Path.Combine(Ih.WebRootPath, "Image/img", Filename), FileMode.Create);
                custom.Image.CopyTo(fs);
                cus.Image = Filename;
                cus.CustomerID=customer.Id;
                _customRepository.Create(cus);
                return RedirectToAction(nameof(Index));
            }
            
            var zone = new SelectList(Enum.GetValues(typeof(Zones)));
            ViewData["Zone"] = zone;
            return View(custom);
        }

        // GET: Customs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var custom = _customRepository.GetById((int)id);
            if (custom == null)
            {
                return NotFound();
            }
           // ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Id", custom.CustomerID);
            return View(custom);
        }
           [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Image,CustomerID")] Custom custom)
        {
            if (id != custom.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomExists(custom.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Id", custom.CustomerID);
            return View(custom);
        }

        // GET: Customs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custom = await _context.Customs
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (custom == null)
            {
                return NotFound();
            }

            return View(custom);
        }

        // POST: Customs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var custom = await _context.Customs.FindAsync(id);
            _context.Customs.Remove(custom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomExists(int id)
        {
            return _context.Customs.Any(e => e.ID == id);
        }
    }
}
