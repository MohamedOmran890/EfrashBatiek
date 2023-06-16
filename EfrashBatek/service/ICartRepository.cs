using EfrashBatek.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface ICartRepository
    {
        ICollection<Cart_Item> LoadFromCookie();

        void AddToCart(int itemID);

        void SaveToCookie();

        void RemoveItem(int itemId);

        void Clear();

        double GetTotal();
        //Cart getCartUser();

        void UpdateItemQuantity(int itemId, int newQuantity);

    }
}