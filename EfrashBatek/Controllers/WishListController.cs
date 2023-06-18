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
		private readonly Context context;
		private readonly IWishListRepository _wishlistRepository; // an object of type IWishListRepository that represents the dependency on the WishListRepository class
        private readonly IdentityRepository _IdentityRepository;
        private readonly ICustomerRepository _customerRepository;
        public WishListController(Context context, IWishListRepository wishlistRepository,IdentityRepository identityRepository,ICustomerRepository customerRepository)
        {
			this.context = context;
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
            var ans = _wishlistRepository.GetCustomerWithUser(userId);
            if(ans == null)
            {
                return View(new WishList());
            }
            var wishlist = _wishlistRepository.GetById(ans.WishList.ID);

            return View(wishlist);
        }



        [HttpPost]
        public IActionResult Create(WishList wishlist)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.Session.GetInt32("CustomerId") ?? 0;

                wishlist.CustomerID = customerId;

                _wishlistRepository.Create(wishlist);

                return RedirectToAction(nameof(Index));
            }

            // Return the view with the model if not valid
            return View(wishlist);
        }



        [HttpPost]
        public IActionResult Edit(WishList wishlist)
        {
            if (ModelState.IsValid)
            {
                _wishlistRepository.Update(wishlist);

                return RedirectToAction(nameof(Index));
            }

            return View(wishlist);
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            _wishlistRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public IActionResult AddItemToWishlist(int wishlistId, int itemId)
        {
            _wishlistRepository.AddItemToWishlist(wishlistId, itemId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddItemToCart(int wishlistId, int itemId)
        {
            _wishlistRepository.AddItemToCart(wishlistId, itemId);

            return RedirectToAction(nameof(Index));
        }


        //[HttpPost]
        //public IActionResult AddAllToCart(int wishlistId)
        //{
        //    var customerId = HttpContext.Session.GetInt32("CustomerId") ?? 0;
        //    var cartId = _context.Carts.FirstOrDefault(c => c.CustomerID == customerId).ID;
        //    _wishlistRepository.AddAllToCart(wishlistId, cartId);

        //    return RedirectToAction(nameof(Index));
        //}



        [HttpPost]
        public IActionResult DeleteFromWishlist(int wishlistId, int itemId)
        {
            _wishlistRepository.DeleteFromWishlist(wishlistId, itemId);

            return RedirectToAction(nameof(Index));
        }

    }
}
                   