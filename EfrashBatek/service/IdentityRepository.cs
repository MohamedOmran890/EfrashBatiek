using EfrashBatek.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EfrashBatek.service
{
	public class IdentityRepository : IIdentityRepository 
	{
		private readonly Context _context;
	
		private readonly IHttpContextAccessor contextAccessor;

		public UserManager<User> UserManager { get; }

		public IdentityRepository(Context context,  UserManager<User> _userManager, IHttpContextAccessor contextAccessor)
		{
			_context = context;
			UserManager = _userManager;
			this.contextAccessor = contextAccessor;
		}

		public string GetUserID ()
		{
			return  contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		public string GetUserName()
		{
			return  contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Name);

		}
		public User GetUser()
		{
			User user =  UserManager.FindByIdAsync(GetUserID()).Result;	
			return user;

		}

		
	}
}
