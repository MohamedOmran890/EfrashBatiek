using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IDesignRepository
    {
        void Create(Design design);
        int Delete(int Id);
        List<Design> GetAll();
        Design GetById(int Id);
        int Update(int id, Design design);
    }
}