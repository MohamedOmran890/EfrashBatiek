﻿using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class OrderRepository : IOrderRepository
    {
        Context context;
        public OrderRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Order order)
        {
            context.Orders.Add(order);

        }
        public int Update(int id, Order order)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == id);
            ans.AddressID = order.AddressID;
            ans.CustomerID = order.CustomerID;
            ans.OrderCode = order.OrderCode;
            ans.OrderDate = order.OrderDate;
            ans.Order_Item = order.Order_Item;
            ans.PaymentMethod = order.PaymentMethod;
            context.Orders.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == Id);
            context.Orders.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Order GetById(int Id)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Order> GetAll()
        {
            var ans = context.Orders.ToList();
            return ans;
        }
    }
}
