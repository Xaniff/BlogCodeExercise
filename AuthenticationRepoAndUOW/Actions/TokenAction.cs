using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DomainClasses.Objects;
using JWT;
using Utilities;

namespace UserRepoAndUOW.Actions
{
	public class TokenAction
	{
		private ITokenProcessorAction _processorAction;

		public TokenAction(ITokenProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Revokes all existing tokens for the specified user.
		/// </summary>
		/// <remarks>
		/// Usually, this will be only one token, but it will address all of them anyway.
		/// </remarks>
		/// <param name="username">String identifying the user.</param>
		public void RevokeAnyTokens(string username)
		{
			_processorAction.RevokeAnyTokens(username);
		}

		/// <summary>
		/// Retrieves the token associated with the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string RetrieveCurrentToken(string username)
		{
			var result = _processorAction.RetrieveCurrentToken(username);
			return result;
		}

		/// <summary>
		/// Creates a new token for the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string AssignToken(string username)
		{
			var result = _processorAction.AssignToken(username);
			return result;
		}

		/// <summary>
		/// Validate that the token is valid.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <param name="hashValue">Value containing the hash value for the user.</param>
		/// <returns></returns>
		public bool ValidateToken(string username, string hashValue)
		{
			var result = _processorAction.ValidateToken(username, hashValue);
			return result;
		}

		/// <summary>
		/// Given the identity of a user, this will revoke any existing tokens associated with the
		/// user and cause a new one to be assigned.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		public string InvalidateAndReissueToken(string username)
		{
			var result = _processorAction.InvalidateAndReissueToken(username);
			return result;
		}

		public CustomCookiePrincipal ExtractCustomCookiePrincipal(HttpContext context)
		{
			//First, check that there are any headers at all
			if (!context.Request.Headers.HasKeys())
				return null;

			//Make sure that the "Token" header exists
			if (context.Request.Headers.GetValues("Token") == null)
				return null;

			//Retrieve the token (it's there) and ensure that it's not null
			var token = Convert.ToString(context.Request.Headers.GetValues("Token").FirstOrDefault());
			if (token == null)
				return null;

			//Extract the JWT secret from the Web.config
			var secret = ConfigurationManager.AppSettings["JwtTokenSecret"];
			if (string.IsNullOrWhiteSpace(secret))
				return null;

			var decodedString = JsonWebToken.Decode(token, secret);
			var decodedToken = JsonUtils<CustomCookiePrincipal>.MapFromJson(decodedString);

			return decodedToken ?? null;
		}

		/// <summary>
		/// Given a token, this will authenticate that the token is legitimate and that
		/// the hash checks out.
		/// </summary>
		/// <param name="context">Current HTTP context.</param>
		/// <returns></returns>
		public HttpStatusCode ValidateToken(HttpContext context)
		{
			//First, check that there are any headers at all
			if (!context.Request.Headers.HasKeys())
				return HttpStatusCode.BadRequest;

			//Make sure that the "Token" header exists
			if (context.Request.Headers.GetValues("Token") == null)
				return HttpStatusCode.BadRequest;

			//Retrieve the token (it's there) and ensure that it's not null
			var token = Convert.ToString(context.Request.Headers.GetValues("Token").FirstOrDefault());
			if (token == null)
				return HttpStatusCode.ExpectationFailed;

			//Extract the JWT secret from the Web.config file
			var secret = ConfigurationManager.AppSettings["JwtTokenSecret"];

			if (string.IsNullOrWhiteSpace(secret))
				return HttpStatusCode.InternalServerError;

			string decodedString;
			try
			{
				decodedString = JsonWebToken.Decode(token, secret);
			}
			catch
			{
				return HttpStatusCode.InternalServerError;
			}

			var decodedToken = JsonUtils<CustomCookiePrincipal>.MapFromJson(decodedString);
			if (decodedToken == null)
				return HttpStatusCode.Unauthorized;

			//Authenticate the hash within the token with the data store to ensure that it's current
			var result = ValidateToken(decodedToken.Username, decodedToken.AuthKey);

			//If the result is true, it's a success
			return result ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;
		}
	}
}
