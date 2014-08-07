using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainClasses.Enums;
using DomainClasses.Interfaces;

namespace DomainClasses.Entities
{
	public class LoginKeyStore : IObjectWithState
	{
		/// <summary>
		/// Primary key of the <see cref="LoginKeyStore">LoginKeyStore</see> object.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The user profile the login key belongs to.
		/// </summary>
		public virtual UserProfile UserProfile { get; set; }

		/// <summary>
		/// The key value being stored.
		/// </summary>
		[StringLength(128)]
		public string AuthKey { get; set; }

		private DateTime _createdOn = DateTime.UtcNow;
		/// <summary>
		/// Date when the login key is created.
		/// </summary>
		public DateTime CreatedOn
		{
			get { return _createdOn; }
			set { _createdOn = value; }
		}

		[NotMapped]
		public ObjectState ObjectState { get; set; }
	}
}
