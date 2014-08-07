namespace DomainClasses.Objects
{
	public class Registration
	{
		/// <summary>
		/// The username used to identify the account externally.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// The password that the user wishes to use with their account.
		/// </summary>
		public string Password { get; set; }
	}
}
