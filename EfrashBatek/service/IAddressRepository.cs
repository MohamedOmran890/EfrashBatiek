using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IAddressRepository
    {
        void Create(Address address);
        int Delete(int Id);
        List<Address> GetAll();
        Address GetById(int Id);
        int Update(int id, Address address);
    }
}