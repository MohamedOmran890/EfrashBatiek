using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class Cart_ItemRepository : ICart_ItemRepository
    {
        Context context;
        
        public Cart_ItemRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Cart_Item item)
        {
            context.Cart_Items.Add(item);
        }
        /* I Dont Know 
         * public int Update(int id, Cart_Item Cart_Item)
          {
              var ans = context.Carts.FirstOrDefault(x => x.ID == id);
              ans.Date = Cart_Item.;
              context.Carts.Update(ans);
              int num = context.SaveChanges();
              return num;
          }
          public int Delete(int Id)
          {
              var ans = context.Cart_Items.FirstOrDefault(x=>x.);
              context.Carts.Remove(ans);
              int num = context.SaveChanges();
              return num;
          }
          public Cart GetById(int Id)
          {
              var ans = context.Carts.FirstOrDefault(x => x.ID == Id);
              return ans;
          }
          public List<Cart> GetAll()
          {
              var ans = context.Carts.ToList();
              return ans;
          }*/
    }
}
