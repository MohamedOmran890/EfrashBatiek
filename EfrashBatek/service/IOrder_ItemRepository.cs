using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IOrder_ItemRepository
    {
        void Create(Order_Item orderitem);

        int Delete(int Id);
        List<Order_Item> GetAll();
        Order_Item GetById(int Id);
        int Update(int id, Order_Item order_item);
    }
}