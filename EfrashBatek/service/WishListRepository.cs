using Castle.Core.Resource;
using EfrashBatek.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EfrashBatek.service
{
    public class WishListRepository : IWishListRepository
    {
        Context context;
        public WishListRepository(Context context)
        {
            this.context = context;
        }
        public WishList Create(WishList wishList)
        {

            context.WishLists.Add(wishList);
            context.SaveChanges();
            return wishList;
        }
        public WishList Update(WishList wishlist)
        {
            context.Update(wishlist);
            context.SaveChanges();
            return wishlist;
        }
        public void Delete(int Id)
        {
            var wishlist = context.WishLists.FirstOrDefault(x => x.ID == Id);
            context.WishLists.Remove(wishlist);
            context.SaveChanges();

        }

        public WishList GetById(int id)
        {
            // Find the wishlist that matches the id and include its related items and customer
            return context.WishLists
                .Include(w => w.Items)
                .Include(w => w.Customer)
                .FirstOrDefault(w => w.ID == id);
        }

        public Customer GetCustomerWithUser(string user)
        {
            var chechk = context.Customers.FirstOrDefault(x => x.UserId == user);
            return chechk;
        }




        public void AddItemToWishlist(int wishlistId, int itemId)
        {
            var wishlist = context.WishLists.Find(wishlistId);

            if (wishlist != null)
            {
                var item = context.Items.Find(itemId);

                // If the item exists and is not already in the wishlist
                if (item != null && !wishlist.Items.Contains(item))
                {
                    wishlist.Items.Add(item);

                    Update(wishlist);
                }
            }
        }


        public void AddItemToCart(int wishlistId, int itemId)
        {
            var wishlist = context.WishLists.Find(wishlistId);

            if (wishlist != null)
            {
                Item item = context.Items.Find(itemId);

                // If the item exists and is in the wishlist
                if (item != null && wishlist.Items.Contains(item))
                {
                    wishlist.Items.Remove(item);
                    Update(wishlist);
                    // Find or create a cart for the current user
                    Cart cart = context.Carts.FirstOrDefault(c => c.CustomerID == wishlist.CustomerID) ?? new Cart
                    {
                        CustomerID = wishlist.CustomerID
                    };

                    bool found = false;
                    foreach (var Item in cart.items)
                    {
                        if (itemId == Item.ItemID)
                        {
                            found = true;
                            Item.Quantity += 1;
                            break;
                        }

                    }
                    if (found == false)
                    {
                        Cart_Item cartItem = new Cart_Item();
                        cartItem.ItemID = itemId;
                        cartItem.CartID = cart.ID;
                        cartItem.Quantity = 1;
                        cart.items.Add(cartItem);

                    }

                    context.SaveChanges();
                }
            }
        }

        // Add all items from wishlist to cart
        public void AddAllToCart(int WishlistId, int CartId)
        {
            var wishlist = context.WishLists.Find(WishlistId);
            if (wishlist != null)
            {
                Cart cart = context.Carts.FirstOrDefault(c => c.CustomerID == wishlist.CustomerID && c.ID == CartId); // find a cart for the customer ID and with
                                                                                                                      // a cart ID that matches the provided cartId
                                                                                                                      //Customer could have many carts
                if (cart == null) // if no cart exists
                {
                    cart = new Cart(); // create a new cart object
                    cart.CustomerID = wishlist.CustomerID; // set its customer ID property
                    context.Carts.Add(cart); // add it to the context
                }

                foreach (Item item in wishlist.Items) // loop through each item in the wishlist's items collection
                {
                    bool found = false;
                    foreach (var Item in cart.items)
                    {
                        if (Item.ItemID == item.ID)
                        {
                            found = true;
                            Item.Quantity += 1;
                            break;
                        }

                    }
                    if (found == false)
                    {
                        Cart_Item cartItem = new Cart_Item();
                        cartItem.CartID = cart.ID;
                        cartItem.ItemID = item.ID;
                        cartItem.Quantity = 1;
                        cart.items.Add(cartItem);

                    }
                }

                context.SaveChanges();
            }
        }


        public void DeleteFromWishlist(int wishlistId, int itemId)
        {
            var wishlist = context.WishLists.Find(wishlistId);

            if (wishlist != null)
            {
                var item = context.Items.Find(itemId);

                // If the item exists and is in the wishlist
                if (item != null && wishlist.Items.Contains(item))
                {
                    wishlist.Items.Remove(item);
                    Update(wishlist);
                }
            }
        }
    }
}
