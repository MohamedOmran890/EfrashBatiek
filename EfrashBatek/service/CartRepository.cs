using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class CartRepository : ICartRepository
    {
        Context context;
        public CartRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Cart brand)
        {
            context.Carts.Add(brand);

        }
        public int Update(int id, Cart cart)
        {
            var ans = context.Carts.FirstOrDefault(x => x.ID == id);
            ans.Date = cart.Date;
            context.Carts.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Carts.FirstOrDefault(x => x.ID == Id);
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
        }
    }
}
