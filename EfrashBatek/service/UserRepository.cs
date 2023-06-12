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
       
      public User GetbyID(string id)
        {


			User user = userManager.FindByIdAsync(id ).Result;
			return user;
		}
        public int Update( User user)
        {
            // omran 
            var ans = GetbyID(user.Id);
            ans.FirstName = user.FirstName; 
            ans.LastName = user.LastName;   
           
            ans.Email = user.Email; 
            ans.PhoneNumber = user.PhoneNumber; 

          
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
       
        public List<User> GetAll()
        {
            var ans = context.Users.ToList();
            return ans;
        }
    }
}
