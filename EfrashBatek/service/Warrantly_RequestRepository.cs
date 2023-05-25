using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class Warrantly_RequestRepository : IWarrantly_RequestRepository
    {
        Context context;
        public Warrantly_RequestRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Warrantly_Request warrantly_request)
        {
            context.Warrantl_Requests.Add(warrantly_request);

        }
        public int Update(int id, Warrantly_Request warrantly_request)
        {
            var ans = context.Warrantl_Requests.FirstOrDefault(x => x.ID == id);
            ans.Description = warrantly_request.Description;
            ans.Order_ItemID = warrantly_request.Order_ItemID;
            ans.Reason = warrantly_request.Reason;
            context.Warrantl_Requests.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Warrantl_Requests.FirstOrDefault(x => x.ID == Id);
            context.Warrantl_Requests.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Warrantly_Request GetById(int Id)
        {
            var ans = context.Warrantl_Requests.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Warrantly_Request> GetAll()
        {
            var ans = context.Warrantl_Requests.ToList();
            return ans;
        }
    }
}
