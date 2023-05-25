using EfrashBatek.Models;
using EfrashBatek.service;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.Controllers
{
    public class Contact_UsController : Controller
    {
        IContact_UsRepository contact;
      public Contact_UsController(IContact_UsRepository contact)
        {
            this.contact = contact;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveContact(Contact_Us obj)
        {
            contact.Create(obj);
            return Content("Your Feedback submitted successfully");
            
        }



    }
}
