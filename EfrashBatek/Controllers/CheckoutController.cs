
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;


    public class CheckoutController : Controller
    {
        IAddressRepository addressRepository;
        public CheckoutController(IAddressRepository Address)
        {
            addressRepository = Address;
        }
        public IActionResult Index(string UserId)
        {
        var add = addressRepository.GetAllById(UserId);
            return View(add);
        }
        public IActionResult Address()
        {
        var zone = new SelectList(Enum.GetValues(typeof(Zone)));
        ViewData["Zone"] = zone;
        return View();
        }
        public IActionResult SaveAddress(Address obj)
        {
            addressRepository.Create(obj);
            return RedirectToAction("PaymentMethod");
            /********/
        }
        public IActionResult PaymentMethod()
        {
            return View();
        }
        public IActionResult Confirmation()
        {

            return View();
        }

    }