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
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        //for Customer
        public  IActionResult Design()
        {
            var ans = designRepository.GetAll();
            return View(ans);
        }
        //For Each Designer
        [Authorize(Roles = "Admin,Designer")]
        public IActionResult MyDesign()
        {
            var userid = identityRepository.GetUserID();
            if(userid==null)
            {
                return RedirectToAction("Login", "Account");
            }
          var designs=designRepository.GetById(userid);

            return View(designs);
        }
        [Authorize(Roles ="Designer")]
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
        [Authorize(Roles = "Admin,Designer")]
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
        [Authorize(Roles = "Admin,Designer")]
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
        [Authorize(Roles = "Admin,Designer")]
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

    }
}
