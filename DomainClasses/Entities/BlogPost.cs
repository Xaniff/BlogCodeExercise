using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;
using DomainClasses.Interfaces;

namespace DomainClasses.Entities
{
	public class BlogPost : IObjectWithState
	{
		/// <summary>
		/// Primary key of the <see cref="BlogPost">BlogPost</see> object.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The title of the <see cref="BlogPost">BlogPost</see>.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The message comprising the <see cref="BlogPost">BlogPost</see>.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The date and time when the <see cref="BlogPost">BlogPost</see> was created.
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// The <see cref="UserProfile">UserProfile</see> that authored the <see cref="BlogPost">BlogPost</see> object.
		/// </summary>
		public UserProfile Author { get; set; }

		private List<Comment> _comments = new List<Comment>();
		/// <summary>
		/// All the <see cref="Comment">comments</see> associated with the given blog post.
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
