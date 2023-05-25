using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class Contact_UsRepository : IContact_UsRepository
    {
        Context context;
        public Contact_UsRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Contact_Us contact_us)
        {
            context.Contact_Us.Add(contact_us);
            context.SaveChanges();

        }
        public Contact_Us GetById(int Id)
        {
            var ans = context.Contact_Us.FirstOrDefault(x => x.Id == Id);
            return ans;
        }
        public List<Contact_Us> GetAll()
        {
            var ans = context.Contact_Us.ToList();
            return ans;
        }
    }
}
