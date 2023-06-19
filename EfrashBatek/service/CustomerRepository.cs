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
        private readonly IIdentityRepository identityRepository;

        public CustomerRepository(Context context, IIdentityRepository identityRepository)
        {
            this.context = context;
            this.identityRepository = identityRepository;
        }
        public void Create(Customer customer )
        {
            context.Customers.Add(customer);

        }
        public Customer GetCustomerbyUserId () { 
              var userID = identityRepository.GetUserID();
            return context.Customers.FirstOrDefault(i=>i.UserId == userID);
        
        }
        
        public int  Edit  (Customer customer , int id )
        {
            var ans = context.Customers.FirstOrDefault(x =>x.Id==id);
            ans.User.FirstName =  customer.User.FirstName;
            ans.User.LastName = customer.User.LastName; 
            ans.User.Email = customer.User.Email;   
            ans.User.PhoneNumber = customer.User.PhoneNumber;
            ans.User.zone = customer.User.zone;

            

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
        public Customer GetbyUserId(string Id)
        {
            var userid = context.Customers.FirstOrDefault(x => x.UserId == Id);
            return userid;

        }
    }
}
