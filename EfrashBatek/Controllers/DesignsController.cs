using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Http;

namespace EfrashBatek.Controllers
{
    public class DesignsController : Controller
    {
        private readonly Context _context;
        private readonly DesignRepository designRepository;
        IdentityRepository identityRepository;

        public DesignsController(Context context,DesignRepository designRepository,IdentityRepository identityRepository)
        {
            _context = context;
            this.designRepository = designRepository;
            this.identityRepository = identityRepository;
        }

        // GET: Designs
        public  IActionResult Design()
        {
            var ans = designRepository.GetAll();
            return View(ans);
        }
        public IActionResult MyDesign()
        {
            var userid = identityRepository.GetUserID();
            if(userid==null)
            {
                return RedirectToAction("Login", "Account");
            }
            designRepository.GetById(userid);

            return View();
        }

        public IActionResult Create()
        {
            ViewData["DesignerID"] = new SelectList(_context.designers, "ID", "NationalCardImage");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Design design, List<IFormFile> images)
        {
            var userid = identityRepository.GetUserID();
            if(userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (ModelState.IsValid)
            {
                design.DesignerID= userid;
                designRepository.Create(design);
                return RedirectToAction(nameof(MyDesign));
            }
            return View(design);
        }

        // GET: Designs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var design = await _context.Designs
            //    .Include(d => d.Designer)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            var design = designRepository.GetDesignById((int)id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // GET: Designs/Create
        // GET: Designs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = designRepository.GetDesignById((int)id);
            if (design == null)
            {
                return NotFound();
            }
            return View(design);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Design design)
        {
            if (id != design.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    designRepository.Update(id,design);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                    
                }
                return RedirectToAction(nameof(Design));
            }
            return View(design);
        }

        // GET: Designs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ans = designRepository.Delete((int)id);
            if (ans == 0)
            {
                return NotFound();
            }

            return NotFound();
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var design = await _context.Designs.FindAsync(id);
        //    _context.Designs.Remove(design);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool DesignExists(int id)
        //{
        //    return _context.Designs.Any(e => e.ID == id);
        //}
    }
}
