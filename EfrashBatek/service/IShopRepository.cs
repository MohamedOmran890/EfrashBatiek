using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IShopRepository
    {
        void Create(Shop shop);
        int Delete(int Id);
        List<Shop> GetAll();
        Shop GetById(int Id);
        int Update(int id, Shop shop);
         int TotalShop();
    }
}