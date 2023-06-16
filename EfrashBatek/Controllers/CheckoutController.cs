﻿
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;


    public class CheckoutController : Controller
    {
        IAddressRepository addressRepository;
        IIdentityRepository _identityRepository;
        public CheckoutController(IAddressRepository Address,IIdentityRepository identityRepository)
        {
            addressRepository = Address;
          _identityRepository = identityRepository;
             
        }
    public IActionResult Index()
    {
        var userId = _identityRepository.GetUserID();
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var add = addressRepository.GetbyID(userId);
        return View(add);
    }
    //public IActionResult Address() //old1
    //{
    //    var zone = new SelectList(Enum.GetValues(typeof(Zone)));
    //    ViewData["Zone"] = zone;
    //    return View("Address1");
    //}
    public IActionResult Addresss()//done1
    {
        var zone = new SelectList(Enum.GetValues(typeof(Zone)));
        ViewData["Zone"] = zone;
        return View("Address1");
    }
    //public IActionResult SaveAddress(Address obj)
    //    {

    //        addressRepository.Create(obj);
    //        return RedirectToAction("PaymentMethod");
    //        /********/
    //    }
    public IActionResult SaveAddress(Address obj)
    {

        addressRepository.Create(obj);
        return RedirectToAction("PaymentMethod");
        
    }
    public IActionResult Index1()
    {
        return View();
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