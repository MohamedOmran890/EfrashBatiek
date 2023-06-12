using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IOrderRepository
    {
        void Create(Order order);
        int Delete(int Id);
        List<Order> GetAll();
        Order GetById(int Id);
        int Update(int id, Order order);
        int TotalOrders();
        List<Order> GetOrders();
        List<Order_Item> GetByShop();


    }
}