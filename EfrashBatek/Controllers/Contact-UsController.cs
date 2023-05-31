using EfrashBatek.Migrations;
using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class CheckoutController : Controller
    {
        IAddressRepository addressRepository;
        public CheckoutController(IAddressRepository Address)
        {
            addressRepository = Address;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Address()
        {
            // var ans=addressRepository.GetById();
            return View(/*ans*/);
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
}
