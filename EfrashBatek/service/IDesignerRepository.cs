using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IDesignerRepository
    {
        void Create(Designer designer);
        int Delete(int Id);
        List<Designer> GetAll();
        Designer GetById(int Id);
    }
}