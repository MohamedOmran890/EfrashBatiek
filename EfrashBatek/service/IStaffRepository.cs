using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IStaffRepository
    {
        void Create(Staff staff);
        int Delete(int Id);
        List<Staff> GetAll();
        Staff GetById(int Id);
        int Update(int id, Staff staff);
        int TotalStaff();
    }
}