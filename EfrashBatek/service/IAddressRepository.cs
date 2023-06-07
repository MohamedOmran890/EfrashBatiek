using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IAddressRepository
    {
        void Create(Address address);
        int Delete(string Id);
        List<Address> GetAll();
        List<Address> GetAllById(string Id);
        int Update(string id, Address address);
    }
}