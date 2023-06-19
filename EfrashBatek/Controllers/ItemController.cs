using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using EfrashBatek.Models;
using EfrashBatek.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EfrashBatek.Controllers
{
    public class ItemController : Controller
    {
        private readonly Context context;

        public ItemController(Context context) {
            this.context = context;
        }  
        public IActionResult ProductaDetails(int id )
        {
            var item = context.Items.FirstOrDefault(i=>i.ID == id);
            if (item == null) { return View("error"); }
            foreach(var product  in context.Products)
            {
               
                if(product.ID == item.ProductID ) {
                   item.Product = product; 
                   
                }
            }
         

            return View(item);
        }
        public IActionResult error() { 
            return View();  
        
        
        }
    }
}
