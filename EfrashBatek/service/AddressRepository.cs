using EfrashBatek.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class AddressRepository : IAddressRepository
    {
        Context context;
        public AddressRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Address address)
        {
            context.Addresses.Add(address);

        }
        public int Update(int id, Address address)
        {
            var obj = context.Addresses.FirstOrDefault(x => x.ID == id);
            obj.UserId = address.UserId;
            obj.OrderId = address.OrderId;
            obj.Zone = address.Zone;
            obj.SetDefault = address.SetDefault;
            obj.PostalCode = address.PostalCode;
            obj.phone = address.phone;
            obj.LastName = address.LastName;
            obj.FirstName = address.FirstName;
            context.Addresses.Update(obj);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Addresses.FirstOrDefault(x => x.ID == Id);
            context.Addresses.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Address GetById(int Id)
        {
            var ans = context.Addresses.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Address> GetAll()
        {
            var ans = context.Addresses.ToList();
            return ans;
        }

    }
}
