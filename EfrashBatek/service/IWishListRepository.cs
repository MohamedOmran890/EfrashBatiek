using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IWishListRepository
    {
        void Create(WishList wishList);
        int Delete(int Id);
        List<WishList> GetAll();
        WishList GetById(int Id);
    }
}