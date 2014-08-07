using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;
using DomainClasses.Objects;

namespace UserRepoAndUOW.Actions
{
	public class LoginAction
	{
		private ILoginProcessorAction _processorAction;

		public LoginAction(ILoginProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Given a <see cref="Login">Login</see> model, this verifies that the username and password
		/// of the user match to a valid profile in the data store. If not, it attempts to provide a helpful
		/// error message to identify the problem.
		/// </summary>
		/// <param name="loginModel">The model that contains the username and password to validate against.</param>
		/// <returns>An <see cref="OperationResult">operation result object</see> yielding either success or failure. In 
		/// case of the later, this would also include an error message.</returns>
		public OperationResult ValidateUser(Login loginModel)
		{
			//Let's check to see that this login object is valid
			var isUserValid = _processorAction.ValidateUsernameAndPassword(loginModel.Username, loginModel.Password);

			//If this is valid, we'll go ahead and return a successful result
			if (isUserValid)
				return new OperationResult(OperationEnum.Success);

			//If this isn't valid, we need to figure out why to report it back to the user, but this requires more lookups,
			//so for performance reaons, we put this off to right here.

			//First, let's see if the username exists
			if (!_processorAction.CheckThatUsernameExists(loginModel.Username))
			{
				return new OperationResult(OperationEnum.Failure, "No such user... is it typed correctly?");
			}

			//If we've gotten to this point, it's simply an invalid password
			return new OperationResult(OperationEnum.Failure, "Wrong password. Please try again!");
		}

		/// <summary>
		/// Creates and re-issues (if necessary) the token to be stored within the authentication cookie.
		/// </summary>
		/// <param name="username">Username of the user requesting this.</param>
		/// <returns></returns>
		public string ProcessLoginKeyStore(string username)
		{
			var result = _processorAction.ProcessLoginKeyStore(username);
			return result;
		}
	}
}
