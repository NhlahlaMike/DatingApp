using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
	// Interface Repository to Manupulate User Data from database  (Made to be used by multiple controller classes)
	public interface IDatingRepository
	{
		void Add<T>(T entity) where T : class; // can be used for different methods for adding
		void Delete<T>(T entity) where T : class; // can be used for different methods for deleting
		Task<bool> SaveAll(); // check to see if there is changes to save to the database and detect problems saving those changes
		Task<IEnumerable<User>> GetUsers(); // Get list of users from database
		Task<User> GetUser(int id); // Get one user from database
	}
}
