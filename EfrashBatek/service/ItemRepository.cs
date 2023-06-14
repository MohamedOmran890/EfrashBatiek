using EfrashBatek.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class ItemRepository : IItemRepository
    {
        Context context;
        public ItemRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();

        }
        public List<Item> NewArrivals() {


            // NewArrivals 
            var newarrivals = context.Items.OrderByDescending(item => item.DateTime).Take(4).ToList();

            return newarrivals;
        }
        public List<Item> Trending()
        {
            // trending products 
            var list = context.Items // include product inside  to void null expection 
                         .SelectMany(i => context.Order_Items.Where(oi => oi.ItemID == i.ID), (i, oi) => new { Item = i, Order_Item = oi, productItem = i.Product })
                         .ToList()
                         .GroupBy(ti => ti.Item, ti => ti.Order_Item)
                         .Select(g => new { product = g.Key, numberOfOrders = g.Count(), productItem = g.Key.Product })
                         .OrderByDescending(x => x.numberOfOrders)
                         .Take(4)
                         .ToList();

            List<Item> items = new List<Item>();
            foreach (var item in list)
            {
                items.Add(item.product);

            }


            return items;
        }
        public int Update(int id, Item item)
        {
            var ans = context.Items.FirstOrDefault(x => x.ID == id);
            ans.Name = item.Name;
            ans.Description = item.Description;
            ans.Code = item.Code;
            ans.Price = item.Price;
            ans.Image = item.Image;
            ans.Image2 = item.Image2;
            ans.Image3 = item.Image3;
            ans.Image4 = item.Image4;
            ans.Image5 = item.Image5;
            ans.QuantityInStore = item.QuantityInStore;
            ans.Brand_ID = item.Brand_ID;
            ans.ShopID = item.ShopID;
            ans.ProductID = item.ProductID;
            context.Items.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Items
                .Include(i => i.Brand)
                .Include(i => i.Product)
                .Include(i => i.Shop)
                .FirstOrDefault(m => m.ID == Id); ;
            if (ans != null)
            {
                context.Items.Remove(ans);
                int num = context.SaveChanges();
                return num;
            }
            else
                return 0;
        }
        public Item GetById(int Id)
        {
            var ans = context.Items
                .Include(i => i.Brand)
                .Include(i => i.Product)
                .Include(i => i.Shop)
                .FirstOrDefault(x=>x.ID==Id);
            return ans;
        }
        public IQueryable<Item> GetAll()
        {
            var ans = context.Items.Include(i => i.Brand).Include(i => i.Product).Include(i => i.Shop);
            return ans;
        }
    }
}
