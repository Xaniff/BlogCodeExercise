using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DomainClasses.Enums;
using DomainClasses.Objects;
using JWT;
using Microsoft.AspNet.SignalR.Hubs;
using UserRepoAndUOW.Actions;

namespace Exercise2.Controllers.API
{
	public class AuthenticationApiController : ApiController
	{
		/// <summary>
		/// Checks that the username isn't already taken and if not, registers the user.
		/// </summary>
		/// <param name="registrationModel"><see cref="Registration">Registration model</see> populated by the user.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Register(Registration registrationModel)
		{
			var action = new RegistrationAction(new RegistrationProcessorAction());
			var validationResult = action.ValidateRegistration(registrationModel);

			//Did it pass validation?
			if (validationResult.Result == OperationEnum.Failure)
			{
				return Request.CreateResponse(HttpStatusCode.NotAcceptable, new { error= validationResult.ErrorMessage});
			}

			//Create the user profile
			action.RegisterUser(registrationModel);

			//The user has been successfully created. Let's populate the details we need for the JWT object
			var cookie = new CustomCookiePrincipal { Username = registrationModel.Username };

			//Process the LoginKeyStore data (unique key for the user)
			var loginAction = new LoginAction(new LoginProcessorAction());
			var key = loginAction.ProcessLoginKeyStore(registrationModel.Username);

			//Append the key to the CustomCookiePrincipal
			cookie.AuthKey = key;

			//Retrieve the secret from the web.config file we'll use to encrypt this cookie
			var secret = ConfigurationManager.AppSettings["JwtTokenSecret"];

			SignalRHub.NotifyAddAuthor();

			//Encode the JsonWebToken
			var token = JsonWebToken.Encode(cookie, secret, JwtHashAlgorithm.HS512);
			return Request.CreateResponse(HttpStatusCode.OK, new {token = token, username = registrationModel.Username});
		}

		/// <summary>
		/// Checks that the username and password validate, then responds with a 
		/// web token to use while logged in.
		/// </summary>
		/// <param name="loginModel">The login model populated by the user.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage Login(Login loginModel)
		{
			if (ModelState.IsValid)
			{
				var loginAction = new LoginAction(new LoginProcessorAction());

				//Process the login request
				var loginResult = loginAction.ValidateUser(loginModel);
				if (loginResult.Result == OperationEnum.Failure)
					return Request.CreateResponse(HttpStatusCode.NotAcceptable, new {error = loginResult.ErrorMessage});

				//The user has been successfully authenticated. Let's populate the details we need for the JWT object
				var cookie = new CustomCookiePrincipal {Username = loginModel.Username};

				//Process the LoginKeyStore data (unique key for the user)
				var key = loginAction.ProcessLoginKeyStore(loginModel.Username);

				//Append the key to the CustomCookiePrincipal
				cookie.AuthKey = key;

				//Retrieve the secret from the web.config file we'll use to encrypt this cookie
				var secret = ConfigurationManager.AppSettings["JwtTokenSecret"];

				//Encode the JsonWebToken
				var token = JsonWebToken.Encode(cookie, secret, JwtHashAlgorithm.HS512);

				return Request.CreateResponse(HttpStatusCode.OK, new {token = token, username = loginModel.Username});
			}
			return Request.CreateResponse(HttpStatusCode.NotAcceptable);
		}
	}
}