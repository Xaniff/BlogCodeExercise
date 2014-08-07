using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using UserBoundedContext;
using Utilities;

namespace UserRepoAndUOW.Actions
{
	public class LoginProcessorAction : ILoginProcessorAction
	{
		/// <summary>
		/// Validates whether a given username and password yield a valid login action or not.
		/// </summary>
		/// <param name="username">The username of the profile to check.</param>
		/// <param name="password">The password of the profile to check.</param>
		/// <returns>True: The user has a valid profile. False: The user is either non-existent or deleted.</returns>
		public bool ValidateEmailAddressAndPassword(string username, string password)
		{
			//First, we need to retrieve the salt value associated with the user
			var saltValue = GetUserSaltValue(username);

			//If it's null or whitespace, the value could not be found, so this is unsuccessful
			if (string.IsNullOrWhiteSpace(saltValue))
				return false;

			//Second, we neeed to create the password hash to validate 
			//against the stored database value.
			var passwordHash = Sha256Utils.GetSuperHash(password, saltValue);

			//Third, let's check that the username and password, when used together, yield success.
			var isUserValid = CheckUsernameAndPassword(username, passwordHash);
			return isUserValid;
		}

		/// <summary>
		/// Verifies that a given username and password will produce a valid user profile from the data store.
		/// </summary>
		/// <param name="username">Username of the profile to check.</param>
		/// <param name="password">Password of the profile associated with the username.</param>
		/// <returns>True: The profile exists. False: The profile does not exist.</returns>
		public bool CheckUsernameAndPassword(string username, string password)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					var result = repo.Grab(a => a.Username == username).FirstOrDefault();
					if (result == null) return false;

					var user = result.Passwords.FirstOrDefault(a => a.Passphrase == password);

					//If there are no users here, the check failed
					if (user == null)
						return false;

					//We're good to go!
					return true;
				}
			}
		}

		/// <summary>
		/// Returns the current salt value for the user with the given 
		/// username who has verified their username.
		/// </summary>
		/// <param name="username">Username to use in the lookup.</param>
		/// <returns>Returns a salt value if successful or a null for no value.</returns>
		public string GetUserSaltValue(string username)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					var result = repo.Grab(a => a.Username == username)
						.FirstOrDefault();
					if (result == null) return "";

					var passwords = result.Passwords.FirstOrDefault();

					//If there are no values here, something has gone wrong and we'll simply
					//return a null value. But if there is a value, we'll return it.
					return passwords == null ? null : passwords.Salt;
				}
			}
		}

		/// <summary>
		/// This checks to see that the username exists at all in the data store
		/// If it doesn't exist at all, there's no way that the user could pass authentication.
		/// </summary>
		/// <param name="username">Username to verify.</param>
		/// <returns>True: The username could potentially be valid. False: The username doesn't not exist in the data store at all.</returns>
		public bool CheckThatUsernameExists(string username)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					var result = repo.Grab(a => a.Username == username).FirstOrDefault();
					
					//If there are no usernames returned, this username simply doesn't exist in the data store.
					return result != null;
				}
			}
		}

		/// <summary>
		/// Creates and re-issues (if necessary) the token to be stored within the authentication cookie.
		/// </summary>
		/// <param name="username">Username of the user requesting this.</param>
		/// <returns></returns>
		public string ProcessLoginKeyStore(string username)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					var result = repo.Grab(a => a.Username == username).FirstOrDefault();
					if (result == null)
						return null;

					//Revoke any existing keys
					var tokenAction = new TokenAction(new TokenProcessorAction());
					tokenAction.RevokeAnyTokens(username);

					//Create a new loginkeystore key
					var token = tokenAction.AssignToken(username);

					//Return the AuthKey now
					return token;
				}
			}
		}
	}
}
