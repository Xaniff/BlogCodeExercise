using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Objects;

namespace UserRepoAndUOW.Actions
{
	public class RegistrationAction
	{
		private IRegistrationProcessorAction _processorAction;
		
		public RegistrationAction(IRegistrationProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Registers a given user into the data store.
		/// </summary>
		/// <param name="userRegistration">Model created by the user during registration.</param>
		/// <returns>The Id of the UserProfile object.</returns>
		public int RegisterUser(Registration userRegistration)
		{
			//Register the user.
			var result =_processorAction.RegisterUser(userRegistration);
			return result;
		}

		/// <summary>
		/// Verifies that the username isn't already in use
		/// </summary>
		/// <param name="userRegistration">Registration model populated by the user.</param>
		/// <returns></returns>
		public OperationResult ValidateRegistration(Registration userRegistration)
		{
			var result = _processorAction.ValidateRegistration(userRegistration);
			return result;
		}
	}
}
