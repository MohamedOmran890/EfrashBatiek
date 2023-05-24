using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class DesignRepository : IDesignRepository
    {
        Context context;
        public DesignRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Design design)
        {
            context.Designs.Add(design);

        }
        public int Update(int id, Design design)
        {
            var ans = context.Designs.FirstOrDefault(x => x.ID == id);
            ans.Description = design.Description;
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Designs.FirstOrDefault(x => x.ID == Id);
            context.Designs.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Design GetById(int Id)
        {
            var ans = context.Designs.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Design> GetAll()
        {
            var ans = context.Designs.ToList();
            return ans;
        }
    }
}
