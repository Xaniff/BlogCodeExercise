using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Objects;

namespace UserRepoAndUOW.Actions
{
	public interface IRegistrationProcessorAction
	{
		/// <summary>
		/// Registers a new user into the data store.
		/// </summary>
		/// <param name="userRegistration">Model describing the new user.</param>
		int RegisterUser(Registration userRegistration);

		/// <summary>
		/// Verifies that the username isn't already in use
		/// </summary>
		/// <param name="userRegistration">Registration model populated by the user.</param>
		/// <returns></returns>
		OperationResult ValidateRegistration(Registration userRegistration);
	}
}
