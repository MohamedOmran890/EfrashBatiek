﻿using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        int Delete(int Id);
        int Edit(Customer customer, int id);
        List<Customer> GetAll();
        Customer GetById(int Id);
        Customer GetbyUserId(string userId);
        ICollection<Order> GetOrders(int id);
        int TotalCustomers();
        Order getOrder(int customerID, int OrderID);
        Customer GetCustomerbyUserId();

    }
}