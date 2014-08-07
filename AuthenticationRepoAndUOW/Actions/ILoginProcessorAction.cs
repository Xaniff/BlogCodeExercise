using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace UserRepoAndUOW.Actions
{
	public interface ILoginProcessorAction
	{
		/// <summary>
		/// Validates whether a given username and password yield a valid login 
		/// action or not.
		/// </summary>
		/// <param name="username">Username of the <see cref="UserProfile">UserProfile</see>to check.</param>
		/// <param name="password">Password of the <see cref="UserProfile">UserProfile</see> to check.</param>
		/// <returns></returns>
		bool ValidateUsernameAndPassword(string username, string password);

		/// <summary>
		/// Validates that a given username exists in the data store.
		/// </summary>
		/// <param name="username">Username to look for.</param>
		/// <returns></returns>
		bool CheckThatUsernameExists(string username);

		/// <summary>
		/// Creates and re-issues (if necessary) the token to be stored within the authentication cookie.
		/// </summary>
		/// <param name="username">Username of the user requesting this.</param>
		/// <returns></returns>
		string ProcessLoginKeyStore(string username);
	}
}
