using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IItemRepository
    {
        void Create(Item item);
        int Delete(int Id);
        List<Item> GetAll();
        Item GetById(int Id);
        int Update(int id, Item item);
    }
}