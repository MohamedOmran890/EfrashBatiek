using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IWishListRepository
    {
        WishList Create(WishList wishList);
        WishList Update(WishList wishList);
        void Delete(int Id);
        WishList GetById(int Id);
        Customer GetCustomerWithUser(string user);
        public void AddItemToWishlist(int wishlistId, int itemId);
        public void AddItemToCart(int wishlistId, int itemId);
        public void AddAllToCart(int WishlistId, int CartId);
        public void DeleteFromWishlist(int wishlistId, int itemId);
    }
}
