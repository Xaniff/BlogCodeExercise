using System.ComponentModel.DataAnnotations.Schema;
using DomainClasses.Enums;
using DomainClasses.Interfaces;

namespace DomainClasses.Entities
{
	public class Password : IObjectWithState
	{
		/// <summary>
		/// Primary key of the Password object.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The salted and hashed password of the user that we'll store for authentication purposes.
		/// </summary>
		public string Passphrase { get; set; }

		/// <summary>
		/// The salt value used with this password for security reasons.
		/// </summary>
		public string Salt { get; set; }

		/// <summary>
		/// The <see cref="UserProfile">user profile</see> that the password belong to.
		/// </summary>
		public virtual UserProfile UserProfile { get; set; }

		[NotMapped]
		public ObjectState ObjectState { get; set; }
	}
}
