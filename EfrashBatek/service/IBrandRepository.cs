using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IBrandRepository
    {
        void Create(Brand brand);
        int Delete(int Id);
        List<Brand> GetAll();
        Brand GetById(int Id);
        int Update(int id, Brand brand);
    }
}