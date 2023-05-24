using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class DesignerRepository : IDesignerRepository
    {
        Context context;
        public DesignerRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Designer designer)
        {
            context.designers.Add(designer);

        }
        /*public int Update(int id, Cart cart)
        {
            var ans = context.designers.FirstOrDefault(x=>x.ID==id);
            ans. = cart.Date;
            context.designers.Update(ans);
            int num = context.SaveChanges();
            return num;
        }*/
        public int Delete(int Id)
        {
            var ans = context.designers.FirstOrDefault(x => x.ID == Id);
            context.designers.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Designer GetById(int Id)
        {
            var ans = context.designers.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Designer> GetAll()
        {
            var ans = context.designers.ToList();
            return ans;
        }
    }
}
