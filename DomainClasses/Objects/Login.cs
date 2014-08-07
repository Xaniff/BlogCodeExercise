using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Objects
{
	public class Login
	{
		private string _username = "";
		[Required(ErrorMessage="Who are you?")]
		[Display(Name="Username")]
		public string Username
		{
			get { return _username.ToLower(); }
			set { _username = value; }
		}

		[Display(Name="Password")]
		[Required(ErrorMessage="And how do we know you're you?")]
		public string Password { get; set; }
	}
}
