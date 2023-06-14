using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using EfrashBatek.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class AddressRepository : IAddressRepository
    {
        Context context;
        private readonly UserManager<User> userManager;
        private readonly IIdentityRepository identityRepository;    
        public AddressRepository(Context context, UserManager<User> userManager , IIdentityRepository identityRepository)
        {
            this.context = context;
            this.userManager = userManager;   
            this.identityRepository = identityRepository;   
        }

		public List<Address> GetAll()
		{
			User user = identityRepository.GetUser();
            var ans = context.Addresses.Where(x => x.UserId == user.Id).ToList();   
			return ans;
		}
        public Address GetbyID(int id) {

            var ans = context.Addresses.FirstOrDefault(x => x.ID == id);
            return ans; 
        }
		public int   Create (Address address)
        {
			User user = identityRepository.GetUser();
            address.UserId = user.Id;


			context.Addresses.Add(address);
			int num = context.SaveChanges();
			return num;


		}
        public int  Delete(int id) {
            var ans = context.Addresses.FirstOrDefault(x => x.ID == id);
            context.Addresses.Remove(ans);
            int num = context.SaveChanges();
            return num;


        }

        public List<Address> View ()
        {


           return GetAll(); 
            


        }

        public int Edit (Address address) {

            
            foreach(var addressItem in GetAll()) { 
            
                if (addressItem.ID == address.ID)
                {
                    addressItem.FirstName = address.FirstName;  
                    addressItem.LastName = address.LastName;
                    addressItem.phone = address.phone;
                    addressItem.Zone = address.Zone;    
                    addressItem.FullAddress= addressItem.FullAddress;
                    addressItem.PostalCode = addressItem.PostalCode;


                    context.Addresses.Update(addressItem);
                    int num = context.SaveChanges();
                    return num;


                }
            }
            return 0;
        
        }



    }
}
