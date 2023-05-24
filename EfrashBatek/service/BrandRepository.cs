using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class BrandRepository : IBrandRepository
    {
        Context context;
        public BrandRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Brand brand)
        {
            context.Brands.Add(brand);

        }
        public int Update(int id, Brand brand)
        {
            var ans = context.Brands.FirstOrDefault(x => x.ID == id);
            ans.Name = brand.Name;
            ans.Category = brand.Category;
            ans.logo = brand.logo;
            context.Brands.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Brands.FirstOrDefault(x => x.ID == Id);
            context.Brands.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Brand GetById(int Id)
        {
            var ans = context.Brands.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Brand> GetAll()
        {
            var ans = context.Brands.ToList();
            return ans;
        }
    }
}
