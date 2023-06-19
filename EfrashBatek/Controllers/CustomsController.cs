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
using Microsoft.AspNetCore.Http;

namespace EfrashBatek.Controllers
{
    public class CustomsController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        public readonly ICustomRepository _customRepository;
        private readonly IWebHostEnvironment Ih;
        IIdentityRepository _identityRepository;
        ICustomerRepository _customer;

        public CustomsController(Context context,UserManager<User> userManager,
            ICustomRepository customRepository, IWebHostEnvironment webHostEnvironment, ICustomerRepository customer)
        {
            _context = context;
            _userManager = userManager;
            _customRepository = customRepository;
            Ih = webHostEnvironment;
           
            _customer = customer;
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custom = _context.Customs.FirstOrDefault(c => c.ID == id);
            _context.Customs.Remove(custom);

            _context.SaveChanges();

          

            return RedirectToAction("ViewCustoms", "MyProfile");

        }


        //// GET: Customs
        //public IActionResult  Index()
        //{

        //    var userid = _identityRepository.GetUserID();
        //    if(userid == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    var data = _customRepository.GetByCustomer(userid);
        //    return View(data);
        //}

        //// GET: Customs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var custom = await _context.Customs
        //        .Include(c => c.Customer)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (custom == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(custom);
        //}

        // GET: Customs/Create
        public async Task<IActionResult> Create()
        {
            //var user = await _userManager.GetUserAsync(User);
            var HaveSession = HttpContext.Session.GetString("Id");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var zone= new SelectList(Enum.GetValues(typeof(Zone)));
            ViewData["Zone"] = zone;
            return View();
        }

        // POST: Customs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomVM custom)
        {
            if (ModelState.IsValid)
            {
                Custom cus = new Custom();
                if (custom.Image != null && custom.Image.Length > 0)
                {
                    string Filename3 = Guid.NewGuid().ToString() + custom.Image.FileName;
                    var fs3 = new FileStream(Path.Combine(Ih.WebRootPath, "Image/Items", Filename3), FileMode.Create);
                    custom.Image.CopyTo(fs3);
                    cus.Image = Filename3;
                }

                cus.Name = custom.Name;
                cus.Phone = custom.Phone;
                cus.Description = custom.Description;
                cus.Zone = custom.Zone;
                Customer customer = _customer.GetCustomerbyUserId();
                cus.CustomerID = customer.Id;
                cus.Customer = customer;
                _customRepository.Create(cus);  
             
               
            }
            //ViewData["CustomerID"] = new SelectList(_context.custom.Zone, "Id", "Id", custom.Zone);
            return RedirectToAction("ViewCustoms", "MyProfile");

        }

        //// GET: Customs/Edit/5
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var custom = _customRepository.GetById((int)id);
        //    if (custom == null)
        //    {
        //        return NotFound();
        //    }
        //   // ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Id", custom.CustomerID);
        //    return View(custom);
        //}
        //   [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id,  Custom custom)
        //{
        //    if (id != custom.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(custom);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CustomExists(custom.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Id", custom.CustomerID);
        //    return View(custom);
        //}

        // GET: Customs/Delete/5
     
        //// POST: Customs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var custom = await _context.Customs.FindAsync(id);
        //    _context.Customs.Remove(custom);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CustomExists(int id)
        //{
        //    return _context.Customs.Any(e => e.ID == id);
        //}
    }
}
