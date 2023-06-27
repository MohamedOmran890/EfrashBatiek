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
        private readonly IIdentityRepository identityRepository;

        public UserRepository(Context context , UserManager<User> _userManager , IIdentityRepository identityRepository)
        {
            this.context = context;
            userManager = _userManager;
            this.identityRepository = identityRepository;
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
        public int Update( User userr) // id ??
        {
            // omran 
           // var ans = context.Users.FirstOrDefault(i => i.Id == userr.Id);

            //ans.FirstName = userr.FirstName; 
            //ans.LastName = userr.LastName;   
            // ans.age = userr.age;
           
            //ans.Email = userr.Email; 
            //ans.PhoneNumber = userr.PhoneNumber; 

          
            //if(ans != null )
            context.Users.Update(userr);
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
