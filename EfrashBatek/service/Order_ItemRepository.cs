using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class Order_ItemRepository : IOrder_ItemRepository
    {
        Context context;
        public Order_ItemRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Item item)
        {
            context.Items.Add(item);

        }
        public int Update(int id, Order_Item order_item)
        {
            var ans = context.Order_Items.FirstOrDefault(x => x.ID == id);
            ans.Quantity = order_item.Quantity;
            ans.ItemID = order_item.ItemID;
            ans.ShopID = order_item.ShopID;
            ans.OrderID = order_item.OrderID;
            ans.OrderState = order_item.OrderState;
        
            context.Order_Items.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Order_Items.FirstOrDefault(x => x.ID == Id);
            context.Order_Items.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Order_Item GetById(int Id)
        {
            var ans = context.Order_Items.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Order_Item> GetAll()
        {
            var ans = context.Order_Items.ToList();
            return ans;
        }
    }
}
