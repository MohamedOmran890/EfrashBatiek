using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IWarrantly_RequestRepository
    {
        void Create(Warrantly_Request warrantly_request);
        int Delete(int Id);
        List<Warrantly_Request> GetAll();
        Warrantly_Request GetById(int Id);
        int Update(int id, Warrantly_Request warrantly_request);
    }
}