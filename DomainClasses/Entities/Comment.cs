using System;
using System.ComponentModel.DataAnnotations.Schema;
using DomainClasses.Enums;
using DomainClasses.Interfaces;

namespace DomainClasses.Entities
{
	public class Comment : IObjectWithState
	{
		/// <summary>
		/// Primary key of the <see cref="Comment">Comment</see> object.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The <see cref="UserProfile">UserProfile</see> that authored the <see cref="Comment">Comment</see>.
		/// </summary>
		public UserProfile Author { get; set; }

		/// <summary>
		/// The message comprising the <see cref="Comment">Comment</see>.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The blog posting that the comment is associated with.
		/// </summary>
		public virtual BlogPost Post { get; set; }

		/// <summary>
		/// The date and time when the <see cref="CreatedOn">CreatedOn</see> was created.
		/// </summary>
		public DateTime CreatedOn { get; set; }

		[NotMapped]
		public ObjectState ObjectState { get; set; }
	}
}
