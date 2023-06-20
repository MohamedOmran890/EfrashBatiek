using EfrashBatek.Models;
using EfrashBatek.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EfrashBatek.Controllers
{
    [Authorize] // this attribute requires the user to be logged in to access the controller's methods
    public class WishListController : Controller
    {
        private readonly Context context;
        private readonly IWishListRepository _wishlistRepository; // an object of type IWishListRepository that represents the dependency on the WishListRepository class
        private readonly IIdentityRepository _IdentityRepository;
        private readonly ICustomerRepository _customerRepository;
        public WishListController(Context context, IWishListRepository wishlistRepository,IIdentityRepository identityRepository,ICustomerRepository customerRepository)
        {
            this.context = context;
            _wishlistRepository = wishlistRepository;
            _IdentityRepository = identityRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index1() { 
          return View();    
        }
        // Action that displays the wishlist of the current customer
        public IActionResult Index()
        {
            ////
            var HaveSession = HttpContext.Session.GetString("Id");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = _IdentityRepository.GetUserID();
            // var customerId = _customerRepository.GetbyUserId(userId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var ans = _wishlistRepository.GetCustomerWithUser(userId);
            if (ans == null)
            {
                return View("Index1" , new WishList());
            }
            //var wishListItems = context.WishListItems.Include(w=> w.Item).FirstOrDefault(w => w.WishListId == ans.WishList.Id).WishList.WishListItems;
            var wishListItems = context.WishListItems.Where(x=>x.WishList.CustomerId==ans.Id).ToList();
            foreach(var item in wishListItems) {
                item.Item = context.Items.FirstOrDefault(i => i.ID == item.ItemId);
            }
            return View(wishListItems);
        }

       [HttpPost]
       public IActionResult AddItemToWishlist(int itemID)
        {
            var HaveSession = HttpContext.Session.GetString("Id");
            if (HaveSession == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = _IdentityRepository.GetUserID();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var customer = _wishlistRepository.GetCustomerWithUser(user);
            var wishList = context.Customers.Include(c => c.WishList).FirstOrDefault(c => c.UserId == user).WishList;
            var item = context.Items.FirstOrDefault(i => i.ID == itemID);
            if (wishList == null)
            {
                customer.WishList = new WishList();
                if(customer.WishList.WishListItems == null)
                {
                    customer.WishList.WishListItems = new List<WishListItem>();
                    customer.WishList.WishListItems.Add(new WishListItem
                    {
                        ItemId = itemID,
                        WishList = customer.WishList
                    });

                }
                else
                {
                    customer.WishList.WishListItems.Add(new WishListItem
                    {
                        ItemId = itemID,
                        WishList = customer.WishList
                    });
                }
            }
            else
            {
                if (customer.WishList.WishListItems == null)
                {
                    customer.WishList.WishListItems = new List<WishListItem>();
                    customer.WishList.WishListItems.Add(new WishListItem
                    {
                        ItemId = itemID,
                        WishList = customer.WishList
                    });

                }
                else
                {
                    customer.WishList.WishListItems.Add(new WishListItem
                    {
                        ItemId = itemID,
                        WishList = customer.WishList
                    });
                }
            }
            context.SaveChanges();  

            return RedirectToAction("Index", "WishList");
        }


        //[HttpPost]
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
                   
