using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainClasses.Entities;
using DomainClasses.Enums;
using DomainClasses.Objects;
using UserBoundedContext;
using Utilities;

namespace UserRepoAndUOW.Actions
{
	public class RegistrationProcessorAction : IRegistrationProcessorAction
	{
		/// <summary>
		/// Registers a new user within the data store.
		/// </summary>
		/// <param name="userRegistration">Registration model populated by a user.</param>
		public int RegisterUser(Registration userRegistration)
		{
			//Create the password hashes require
			//First, let's create the salt we'll need
			var passwordSalt = Sha256Utils.GenerateRandomString(128, true);

			//Now we'll create the hash that'll be stored in the data store
			var passwordHash = Sha256Utils.GetSuperHash(userRegistration.Password, passwordSalt);

			//Create the user profile that we'll be storing in just a bit
			var userProfile = new UserProfile
			{
				Username = userRegistration.Username,
				Passwords = new List<Password>
				{
					new Password
					{
						Passphrase = passwordHash,
						Salt = passwordSalt
					}
				}
			};

			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
				
					//Add the user profile to the repository now
					var profileId = repo.Add(userProfile);
					return profileId;
				}
			}
		}

		/// <summary>
		/// Verifies that the username isn't already in use
		/// </summary>
		/// <param name="userRegistration">Registration model populated by the user.</param>
		/// <returns></returns>
		public OperationResult ValidateRegistration(Registration userRegistration)
		{
			using (var uow = new UnitOfWork<UserContext>())
			{
				using (var repo = new UserRepository(uow))
				{
					var matches = repo.Contains(a => a.Username == userRegistration.Username);

					return matches
						? new OperationResult(OperationEnum.Failure, "A profile already exists with that username") //There are matches
						: new OperationResult(); //No matches
				}
			}
		}
	}
}
