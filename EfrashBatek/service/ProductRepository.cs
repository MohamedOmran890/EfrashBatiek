using EfrashBatek.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EfrashBatek.service
{
    public class ProductRepository : IProductRepository
    {
        Context context;
        public ProductRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();

        }
       
        public int Update(int id, Product product)
        {
            var ans = context.Products.FirstOrDefault(x => x.ID == id);
            ans.Description = product.Description;
            ans.ProductName = product.ProductName;
            ans.Category = product.Category;
            context.Products.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Products.FirstOrDefault(x => x.ID == Id);
            context.Products.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Product GetById(int Id)
        {
            var ans = context.Products.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Product> GetAll()
        {
            var ans = context.Products.ToList();
           
            return ans;
        }
	
		public List<Product> GetBy()
        {
            var ans = context.Products.ToList();
            return ans;
        }
       public List<Product> GetByCategory(Category Category)
        {
            var ans = context.Products.Where(x => x.Category == Category).ToList();
            return ans;
        }
    }
}
