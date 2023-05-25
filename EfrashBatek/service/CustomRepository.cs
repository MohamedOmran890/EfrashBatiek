using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class CustomRepository : ICustomRepository
    {
        Context context;
        public CustomRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Custom custom)
        {
            context.Customs.Add(custom);
            context.SaveChanges();

        }
        public int Update(int id, Custom custom)
        {
            var ans = context.Customs.FirstOrDefault(x => x.ID == id);
            ans.Description = custom.Description;
            ans.Image = custom.Image;
            context.Customs.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Customs.FirstOrDefault(x => x.ID == Id);
            context.Customs.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Custom GetById(int Id)
        {
            var ans = context.Customs.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Custom> GetAll()
        {
            var ans = context.Customs.ToList();
            return ans;
        }
    }
}
