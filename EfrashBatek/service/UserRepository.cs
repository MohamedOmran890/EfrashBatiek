using EfrashBatek.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class UserRepository : IUserRepository
    {
        Context context;
        private readonly UserManager<User> userManager;

        public UserRepository(Context context , UserManager<User> _userManager)
        {
            this.context = context;
            userManager = _userManager;
        }
        public void Create(User user)
        {
            context.Users.Add(user);

        }
        public User  getbyidentity(string identity) {

            User user =  userManager.FindByIdAsync(identity).Result;
            return user;    
        }
        public int  edit (User user , string id ) {

            // alyaa 
            var user2 = getbyidentity(id);

            user2.FirstName = user.FirstName;
            user2.LastName = user.LastName;
            user2.Email = user.Email;
            user2.PhoneNumber = user.PhoneNumber;

            context.Users.Update(user2);
            int num = context.SaveChanges();
            return num;

        }
        public int Update(string id, User user)
        {
            // omran 
            var ans = context.Users.FirstOrDefault(x => x.Id == id);
           
          
            if(ans != null )
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
