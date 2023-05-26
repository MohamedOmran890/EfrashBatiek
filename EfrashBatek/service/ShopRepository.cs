using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class ShopRepository : IShopRepository
    {
        Context context;
        public ShopRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Shop shop)
        {
            context.Shops.Add(shop);

        }
        public int Update(int id, Shop shop)
        {
            var ans = context.Shops.FirstOrDefault(x => x.ID == id);
            ans.Name = shop.Name;
            ans.ShopNumber = shop.ShopNumber;
            ans.ShopAddress = shop.ShopAddress;
            ans.TaxCardImage = shop.TaxCardImage;
            context.Shops.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Shops.FirstOrDefault(x => x.ID == Id);
            context.Shops.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Shop GetById(int Id)
        {
            var ans = context.Shops.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Shop> GetAll()
        {
            var ans = context.Shops.ToList();
            return ans;
        }
        public int TotalShop()
        {
            var ans = context.Shops.Count();
            return ans;
        }
    }
}
