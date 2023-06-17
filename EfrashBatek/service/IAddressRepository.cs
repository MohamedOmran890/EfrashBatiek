using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IAddressRepository
    {
       int Edit(Address address);
        List<Address> View();
        int  Create(Address address);
        public Address GetbyID(int  id);
        public int  Delete (int id );
    }
}