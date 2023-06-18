using EfrashBatek.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;


namespace EfrashBatek.service
{
    public class CartRepository : ICartRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;


        Context context;
        public ICollection<Cart_Item> cart { get; set; }

        public CartRepository(IHttpContextAccessor httpContextAccessor, Context context)
        {

            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            LoadFromCookie();
        }
        public ICollection<Cart_Item> LoadFromCookie()
        {
            var cookieData = httpContextAccessor.HttpContext.Request.Cookies["mycart"];
            if (!string.IsNullOrEmpty(cookieData))
            {
                cart = JsonConvert.DeserializeObject<ICollection<Cart_Item>>(cookieData);
            }
            else
            {
                cart = new List<Cart_Item>();
            }
            return cart;

        }

        public void AddToCart(int itemID)
        {
            Cart_Item existingItem = null;
            if (cart != null)
            {
                existingItem = cart.FirstOrDefault(i => i.ItemID == itemID);

            }
            else
            {
                cart = new List<Cart_Item>();
            }



            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                
                // assign id to cookie 
                Cart_Item itemItem = new Cart_Item { Quantity = 1, ItemID = itemID , CartID=1 };

                cart.Add(itemItem);
            }
            SaveToCookie();
        }

        public void SaveToCookie()
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true,
                HttpOnly = true
            };

            var cookieData = JsonConvert.SerializeObject(cart);
            httpContextAccessor.HttpContext.Response.Cookies.Append("mycart", cookieData, options);
        }

        public void RemoveItem(int itemId)
        {
            var itemToRemove = cart.FirstOrDefault(i => i.ItemID == itemId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveToCookie();
            }
        }
        public void Clear()
        {
            cart = new List<Cart_Item>();
            SaveToCookie();
        }
        public double GetTotal()
        {
            return cart.Sum(i => i.Item.Price * i.Quantity);
        }
        public void UpdateItemQuantity(int itemId, int newQuantity)
        {
            var itemToUpdate = cart.FirstOrDefault(i => i.ItemID == itemId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = newQuantity;
                SaveToCookie();
            }
        }

    }
}
