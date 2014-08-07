using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UserRepoAndUOW.Actions
{
	public interface ITokenProcessorAction
	{
		/// <summary>
		/// Revokes all existing tokens for the specified user.
		/// </summary>
		/// <remarks>
		/// Usually, this will be only one token, but it will address all of them anyway.
		/// </remarks>
		/// <param name="username">String identifying the user.</param>
		void RevokeAnyTokens(string username);

		/// <summary>
		/// Retrieves the token associated with the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		string RetrieveCurrentToken(string username);

		/// <summary>
		/// Creates a new token for the specified user.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		string AssignToken(string username);

		/// <summary>
		/// Validate that the token is valid.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <param name="hashValue">Value containing the hash value for the user.</param>
		/// <returns></returns>
		bool ValidateToken(string username, string hashValue);


		/// <summary>
		/// Given the identity of a user, this will revoke any existing tokens associated with the
		/// user and cause a new one to be assigned.
		/// </summary>
		/// <param name="username">String identifying the user.</param>
		/// <returns></returns>
		string InvalidateAndReissueToken(string username);
	}
}
