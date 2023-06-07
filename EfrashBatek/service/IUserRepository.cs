using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
	public interface IUserRepository
	{
		void Create(User user);
		int Delete(string Id);
		List<User> GetAll();
		User GetbyID(string id);
		int Update(User user);



	}
}