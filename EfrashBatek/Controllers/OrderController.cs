using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Authorization;

namespace EfrashBatek.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly Context _context;
        private IOrderRepository _orderRepository;
        public OrderController(Context context,IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        public IActionResult OrderAdmin()
        {
            var ans = _orderRepository.GetAll();
            return View( ans);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ans = _orderRepository.GetById((int)id);
            if (ans == null)
            {
                return NotFound();
            }
            return View(ans);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ans = _orderRepository.GetById((int)id);
            if (ans == null)
            {
                return NotFound();
            }
            var zone = new SelectList(Enum.GetValues(typeof(Zone)));
            ViewData["Zone"] = zone;
            return View(ans);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderRepository.Update(id,order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (OrderExists(order.ID)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(OrderAdmin));
            }
            var zone = new SelectList(Enum.GetValues(typeof(Zone)));
            ViewData["Zone"] = zone;
            return View(order);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ans = _orderRepository.Delete((int)id);
            if (ans == 0)
            {
                return NotFound();
            }

            return View(ans);
        }

        private Order OrderExists(int id)
        {
            return _orderRepository.GetById(id);
        }
    }
}
