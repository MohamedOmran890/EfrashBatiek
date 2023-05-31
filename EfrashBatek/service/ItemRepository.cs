using EfrashBatek.Models;
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
        public int Update(int id, Item item)
        {
            var ans = context.Items.FirstOrDefault(x => x.ID == id);
            ans.Name = item.Name;
            ans.Description = item.Description;
            ans.Code = item.Code;
            ans.Price = item.Price;
            ans.Image = item.Image;
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
            var ans = context.Items.FirstOrDefault(x => x.ID == Id);
            context.Items.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Item GetById(int Id)
        {
            var ans = context.Items.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Item> GetAll()
        {
            var ans = context.Items.ToList();
            return ans;
        }
    }
}
