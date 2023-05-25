using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Mvc;
namespace EfrashBatek.Controllers
{
    public class CategoryController : Controller
    {
        private IProductRepository ProductRepository;
        public CategoryController(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }
        public IActionResult Index(Category Category)
        {
           var ans=ProductRepository.GetByCategory(Category);
            return View(ans);
        }
    }
}
