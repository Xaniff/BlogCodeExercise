using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DomainClasses.Enums;
using DomainClasses.Interfaces;

namespace DomainClasses.Entities
{
	public class UserProfile : IObjectWithState
	{
		/// <summary>
		/// Primary key of the UserProfile object.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Username belonging to the user.
		/// </summary>
		public string Username { get; set; }

		private List<Password> _passwords = new List<Password>();
		/// <summary>
		/// The collection of the <see cref="Password">Passwords</see> for the user.
		/// </summary>
		public List<Password> Passwords
		{
			get { return _passwords; }
			set { _passwords = value; }
		}

		private List<LoginKeyStore> _loginKeys = new List<LoginKeyStore>(); 
		/// <summary>
		/// The collection of the <see cref="LoginKeyStore">Login keys</see> for the user.
		/// </summary>
		public virtual List<LoginKeyStore> LoginKeys
		{
			get { return _loginKeys; }
			set { _loginKeys = value; }
		} 

		private List<BlogPost> _blogPosts = new List<BlogPost>();
		/// <summary>
		/// The collection of the <see cref="BlogPost">BlogPosts</see> written by the user.
		/// </summary>
		public List<BlogPost> BlogPosts
		{
			get { return _blogPosts; }
			set { _blogPosts = value; }
		}

		private List<Comment> _comments = new List<Comment>();
		/// <summary>
		/// The collection of the <see cref="Comment">Comments</see> written by the user.
		/// </summary>
		public List<Comment> Comments
		{
			get { return _comments; }
			set { _comments = value; }
		}
			
		[NotMapped]
		public ObjectState ObjectState { get; set; }
	}
}
