using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface ICustomRepository
    {
        void Create(Custom custom);
        int Delete(int Id);
        List<Custom> GetAll();
        Custom GetById(int Id);
        int Update(int id, Custom custom);
    }
}