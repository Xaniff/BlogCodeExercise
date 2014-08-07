using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainClasses.Entities;
using UserBoundedContext;
using Utilities;

namespace UserRepoAndUOW.Actions
{
	public class TokenProcessorAction : ITokenProcessorAction
	{
		/// <summary>
		/// Revokes all existing tokens for the specified user.
		/// </summary>
		/// <remarks>
		/// Usually, this will be only one token, but it will address all of them anyway.
		/// </remarks>
		/// <param name="username">String identifying the user.</param>
		public void RevokeAnyTokens(string username)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					//Retrieves the user from the data store
					var user = repo.Grab(a => a.Username == username)
						.FirstOrDefault();
					if (user == null) return;

					//If there are no login keys associated, this method can't do anything
					if (!user.LoginKeys.Any())
						return;

					//Retrieve all the associated keys that have not been deleted
					var keys = user.LoginKeys.ToList();
					repo.RemoveManyLoginKeyStores(keys);
				}
			}
		}

		/// <summary>
		/// Retrieves the token associated with the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string RetrieveCurrentToken(string username)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					//Retrieve the specified user from the data store
					var user = repo.Grab(a => a.Username == username).FirstOrDefault();
					if (user == null) return null;

					//Determine if there are any tokens assigned to the user
					if (!user.LoginKeys.Any())
					{
						//Create a new token
						var token = AssignToken(username);
						return token;
					}
					else
					{
						//Retrieve the keys
						var keys = user.LoginKeys.ToList();

						//If there are none, create a new token - this should already have been covered, but it's here just in case
						if (!keys.Any())
						{
							var token = AssignToken(username);
							return token;
						}

						//Get the first one
						var first = keys.FirstOrDefault();

						//If this is null, create a new token
						if (first == null)
						{
							var token = AssignToken(username);
							return token;
						}

						return first.AuthKey;
					}
				}
			}
		}

		/// <summary>
		/// Creates a new token for the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string AssignToken(string username)
		{
			//TODO: Refactor the random string into another class.. say, RandomUtils.
			var newToken = Sha256Utils.GenerateRandomString(128);

			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					//Retrieve the specified user from the data store.
					var user = repo.Grab(a => a.Username == username).FirstOrDefault();
					if (user == null) return null;

					var createdOn = DateTime.UtcNow;
					//Create the new key for the specified user
					var loginKey = new LoginKeyStore
					{
						CreatedOn = createdOn,
						UserProfile = user,
						AuthKey = newToken
					};

					//Create the new login key
					var loginKeyId = repo.AddLoginKey(loginKey);

					//Retrieve it so we can add it to the user and update it
					var newLoginKey = repo.FindLoginKey(loginKeyId);
					user.LoginKeys.Add(newLoginKey);
					repo.InsertOrUpdate(user);

					return newToken;
				}
			}
		}

		/// <summary>
		/// Validate that the token is valid.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <param name="hashValue">Value containing the hash value for the user.</param>
		/// <returns></returns>
		public bool ValidateToken(string username, string hashValue)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					//Retrieve the specified user from the data store.
					var user = repo.Grab(a => a.Username == username).FirstOrDefault();
					if (user == null) return false;

					//If there are no valid login keys associated, this method can't do anything
					if (!user.LoginKeys.Any())
						return false;

					//Retrieve any valid keys assigned to the user with this hash value right now
					var loginKeys = user.LoginKeys.Where(a => a.AuthKey == hashValue)
						.ToList();

					//If there are no login keys here, this is obviously false
					if (loginKeys.Count == 0)
						return false;

					//If there is a result, it matches all necessary conditions, so return true;
					return true;
				}
			}
		}

		/// <summary>
		/// Given the identity of a user, this will revoke any existing tokens associated with the user
		/// and cause a new one to be assigned.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string InvalidateAndReissueToken(string username)
		{
			//Revoke any existing tokens associated with the user
			RevokeAnyTokens(username);

			//Re-issue a token to the user
			var token = AssignToken(username);
			return token;
		}
	}
}
