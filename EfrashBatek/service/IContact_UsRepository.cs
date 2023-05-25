using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IContact_UsRepository
    {
        void Create(Contact_Us contact_us);
        List<Contact_Us> GetAll();
        Contact_Us GetById(int Id);
    }
}