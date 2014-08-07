using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Objects
{
	public class Registration
	{
		private string _username = "";
		/// <summary>
		/// The username used to identify the account externally.
		/// </summary>
		public string Username
		{
			get { return _username.ToLower(); }
			set { _username = value ?? ""; }
		}

		/// <summary>
		/// The password that the user wishes to use with their account.
		/// </summary>
		public string Password { get; set; }
	}
}
