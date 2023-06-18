using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public interface IItemRepository
    {
        void Create(Item item);
        int Delete(int Id);
        IQueryable<Item> GetAll();

        Item GetById(int Id);
        int Update(int id, Item item);
        List<Item> NewArrivals();
        List<Item> Trending();
    }
}