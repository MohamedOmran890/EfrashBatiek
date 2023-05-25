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
            ans.BirthDate = user.BirthDate;
            ans.PhoneNumber = user.PhoneNumber;
            ans.Address = user.Address;
            ans.age = user.age;
            ans.Gender = user.Gender;
            ans.UserName = user.UserName;
            ans.Videos = user.Videos;
            ans.UserType = user.UserType;
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
