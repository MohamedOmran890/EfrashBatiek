using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IAdminRepository
    {
        void Create(Admin admin);
        int Delete(int Id);
        List<Admin> GetAll();
        Admin GetById(int Id);
        int Update(int id, Admin admin);
    }
}