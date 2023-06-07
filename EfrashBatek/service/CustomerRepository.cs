using EfrashBatek.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class CustomerRepository : ICustomerRepository
    {
        Context context;
        public CustomerRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Customer customer)
        {
            context.Customers.Add(customer);

        }
        
        public int  Edit  (Customer customer , int id )
        {
            var ans = context.Customers.FirstOrDefault(x =>x.Id==id);
            ans.User.FirstName =  customer.User.FirstName;
            ans.User.LastName = customer.User.LastName; 
            ans.User.Email = customer.User.Email;   
            ans.User.PhoneNumber = customer.User.PhoneNumber;
            ans.User.zone = customer.User.zone;

            ans.User.Address.FirstName = customer.User.Address.FirstName;   
            ans.User.Address.LastName = customer.User.Address.LastName;
            //ans.User.Address.StreetName = customer.User.Address.StreetName;
            //ans.User.Address.ApartmentNumber = customer.User.Address.ApartmentNumber;
            //ans.User.Address.description = customer.User.Address.description;
            

			context.Customers.Update(ans);
			int num = context.SaveChanges();
			return num;


		}
        
        public int Delete(int Id)
        {
            var ans = context.Customers.FirstOrDefault(x => x.Id == Id);
            context.Customers.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Customer GetById(int Id)
        {
            var ans = context.Customers.FirstOrDefault(x => x.Id == Id);
            return ans;
        }
        
        public List<Customer> GetAll()
        {
            var ans = context.Customers.ToList();
            return ans;
        }
        public ICollection<Order> GetOrders (int id)
        {
            var ans = GetById(id);
            var anss = ans.Orders;
            return anss;
        }
        public Order getOrder (int customerID , int OrderID)
        {

            var ans = GetById(customerID);
            var anss = ans.Orders;
            foreach(var order in anss) { 
              if(order.ID == OrderID) return order;
            }
            return null; 

        }
        public int TotalCustomers()
        {
            var ans = context.Customers.Count();
            return ans;
        }
    }
}
