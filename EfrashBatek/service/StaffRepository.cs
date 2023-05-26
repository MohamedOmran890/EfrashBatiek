using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class StaffRepository : IStaffRepository
    {
        Context context;
        public StaffRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Staff staff)
        {
            context.The_Staff.Add(staff);

        }
        public int Update(int id, Staff staff)
        {
            var ans = context.The_Staff.FirstOrDefault(x => x.ID == id);
            ans.ShopID = staff.ShopID;
            context.The_Staff.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.The_Staff.FirstOrDefault(x => x.ID == Id);
            context.The_Staff.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Staff GetById(int Id)
        {
            var ans = context.The_Staff.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Staff> GetAll()
        {
            var ans = context.The_Staff.ToList();
            return ans;
        }
        public int TotalStaff()
        {
            var ans = context.The_Staff.Count();
            return ans;
        }
    }
}
