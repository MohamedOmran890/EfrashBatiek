using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class UserRepository : IUserRepository
    {
        Context context;
        public UserRepository(Context context)
        {
            this.context = context;
        }
        public void Create(User user)
        {
            context.Users.Add(user);

        }
        public int Update(string id, User user)
        {
            var ans = context.Users.FirstOrDefault(x => x.Id == id);

            ans.FirstName = user.FirstName;
            ans.LastName = user.LastName;
            ans.Email = user.Email; 
            ans.PhoneNumber = user.PhoneNumber;
            
            context.Users.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(string Id)
        {
            var ans = context.Users.FirstOrDefault(x => x.Id == Id);
            context.Users.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public User GetById(string Id)
        {
            var ans = context.Users.FirstOrDefault(x => x.Id == Id);
            return ans;
        }
        public List<User> GetAll()
        {
            var ans = context.Users.ToList();
            return ans;
        }
    }
}
