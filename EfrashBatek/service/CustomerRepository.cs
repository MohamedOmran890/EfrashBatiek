using EfrashBatek.Models;
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
        /*
        public int Update(int id, Customer customer)
        {
            var ans = context.Customers.FirstOrDefault(x =>x.Id==id);
            ans.User. = Customer.;
            context.Customers.Update(ans);
            int num = context.SaveChanges();
            return num;
        }*/
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
        public int TotalCustomers()
        {
            var ans = context.Customers.Count();
            return ans;
        }
    }
}
