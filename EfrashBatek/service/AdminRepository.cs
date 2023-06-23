using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class AdminRepository : IAdminRepository
    {
        Context context;
        public AdminRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Admin admin)
        {
            context.Admin.Add(admin);
            context.SaveChanges();

        }
        public int Update(int id, Admin admin)
        {
            var ans = context.Admin.FirstOrDefault(x => x.ID == id);
            ans.User.FirstName = admin.User.FirstName;
            ans.User.LastName = admin.User.LastName;
            ans.User.PasswordHash = admin.User.PasswordHash;
            ans.User.age = admin.User.age;
            ans.User.Email = admin.User.Email;
            ans.User.Gender = admin.User.Gender;
            ans.User.PhoneNumber = admin.User.PhoneNumber;
          
            context.Admin.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Admin.FirstOrDefault(x => x.ID == Id);
            context.Admin.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Admin GetById(int Id)
        {
            var ans = context.Admin.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Admin> GetAll()
        {
            var ans = context.Admin.ToList();
            return ans;
        }

    }
}
