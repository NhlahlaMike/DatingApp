using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	// Interface Repository for User Authorization (Made to be used by multiple controller classes)
	public interface IAuthRepository
	{
		Task<User> Register(User user, string password);
		Task<User> Login(string username, string password);
		Task<bool> UserExists(string username);
	}
}
