using EfrashBatek.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EfrashBatek.service
{
	public interface IIdentityRepository
	{
	 string GetUserID(); 

	 string GetUserName();


     User GetUser();

	}
}
