using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        int Delete(int Id);
        List<Customer> GetAll();
        Customer GetById(int Id);
    }
}