using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace EfrashBatek.Controllers
{
    [Authorize] // this attribute requires the user to be logged in to access the controller's methods
    public class WishListController : Controller
    {
        private readonly IWishListRepository _wishlistRepository; // an object of type IWishListRepository that represents the dependency on the WishListRepository class
        private readonly IIdentityRepository _IdentityRepository;
        private readonly ICustomerRepository _customerRepository;
        public WishListController(Context context, IWishListRepository wishlistRepository,IIdentityRepository identityRepository,ICustomerRepository customerRepository)
        {
            _wishlistRepository = wishlistRepository;
            _IdentityRepository = identityRepository;
            _customerRepository = customerRepository;
        }

        // Action that displays the wishlist of the current customer
        public IActionResult Index()
        {
            // Get the current customer id from the session or authentication
            //var customerId = HttpContext.Session.GetInt32("CustomerId") ?? 0;
            var userId = _IdentityRepository.GetUserID();
            // var customerId = _customerRepository.GetbyUserId(userId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var ans = _wishlistRepository.GetCustomerWithUser(userId);
            if (ans == null)
            {
                return View(new WishList());
            }
            var wishlist = _wishlistRepository.GetByAll(ans.Id);

            return View(wishlist);
        }

        [HttpPost]
       public IActionResult AddProductToWishlist(int productId)
        {
            var user =  _IdentityRepository.GetUserID();
            var customer = _wishlistRepository.GetCustomerWithUser(user);

            var wishlist = new WishList
            {
                ItemId = productId,
                CustomerId = customer.Id,
            };

            _wishlistRepository.Create(wishlist);

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _wishlistRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }




        //[HttpPost]
        //public IActionResult AddItemToWishlist(int wishlistId, int itemId)
        //{
        //    _wishlistRepository.AddItemToWishlist(wishlistId, itemId);

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public IActionResult AddItemToCart(int wishlistId, int itemId)
        //{
        //    _wishlistRepository.AddItemToCart(wishlistId, itemId);

        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpPost]
        //public IActionResult AddAllToCart(int wishlistId)
        //{
        //    var customerId = HttpContext.Session.GetInt32("CustomerId") ?? 0;
        //    var cartId = _context.Carts.FirstOrDefault(c => c.CustomerID == customerId).ID;
        //    _wishlistRepository.AddAllToCart(wishlistId, cartId);

        //    return RedirectToAction(nameof(Index));
        //}



        //[HttpPost]
        //public IActionResult DeleteFromWishlist(int wishlistId, int itemId)
        //{
        //    _wishlistRepository.DeleteFromWishlist(wishlistId, itemId);

        //    return RedirectToAction(nameof(Index));
        //}

    }
}
                   
