using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class OrderRepository : IOrderRepository
    {
        Context context;
        private readonly IIdentityRepository repository;
     
        public OrderRepository(Context context , IIdentityRepository repository )
        {
            this.context = context;
            this.repository = repository;
           
        }
        public void Create(Order order)
        {
            context.Orders.Add(order);
             context.SaveChanges();
           

        }
        public int Update(int id, Order order)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == id);
            ans.AddressID = order.AddressID;
            ans.CustomerID = order.CustomerID;
            ans.OrderCode = order.OrderCode;
            ans.OrderDate = order.OrderDate;
            ans.Order_Item = order.Order_Item;
            ans.PaymentMethod = order.PaymentMethod;
            context.Orders.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == Id);
            context.Orders.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Order GetById(int Id)
        {
            var ans = context.Orders.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
       
        public List<Order> GetAll()
        {
            var ans = context.Orders.ToList();
            return ans;
        }
        public int TotalOrders()
        {
            var ans = context.Orders.Count();
            return ans;
        }

        // my profile 
        public List<Order> GetOrders()
        {
            User user = repository.GetUser();
            int cnt = 0;
            List<Order> ans = new List<Order>();    
            foreach(var item in context.Orders)
            {
                if(item.Customer !=null && item.Customer.UserId ==user.Id) {

                    ans.Add(item);
                
                }
            }
            return ans;

        }

        // my profile 
       
        public List<Order_Item> GetByShop()
        {
            var ans = context.Order_Items.GroupBy(x => x.ShopID);
            return (List<Order_Item>)ans;

        }
    }
}
