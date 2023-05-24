using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class WishListRepository : IWishListRepository
    {
        Context context;
        public WishListRepository(Context context)
        {
            this.context = context;
        }
        public void Create(WishList wishList)
        {
            context.WishLists.Add(wishList);

        }
        /* public int Update(int id, WishList wishList)
         {
             var ans = context.WishLists.FirstOrDefault(x => x.ID == id);
             context.WishLists.Update(ans);
             int num = context.SaveChanges();
             return num;
         }*/
        public int Delete(int Id)
        {
            var ans = context.WishLists.FirstOrDefault(x => x.ID == Id);
            context.WishLists.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public WishList GetById(int Id)
        {
            var ans = context.WishLists.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<WishList> GetAll()
        {
            var ans = context.WishLists.ToList();
            return ans;
        }
    }
}
