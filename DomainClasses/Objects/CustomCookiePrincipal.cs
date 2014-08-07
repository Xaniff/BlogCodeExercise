using Newtonsoft.Json;

namespace DomainClasses.Objects
{
	public class CustomCookiePrincipal
	{
		[JsonProperty("Username")]
		public string Username { get; set; }

		/// <summary>
		/// This is the key that is generated when the cookie is created
		/// that is used to authenticate this user in future requests.
		/// </summary>
		public string AuthKey { get; set; }
	}
}
