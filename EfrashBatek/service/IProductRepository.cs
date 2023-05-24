using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IProductRepository
    {
        void Create(Product product);
        int Delete(int Id);
        List<Product> GetAll();
        Product GetById(int Id);
        int Update(int id, Product product);
    }
}