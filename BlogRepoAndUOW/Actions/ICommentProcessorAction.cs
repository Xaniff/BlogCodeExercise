using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public interface ICommentProcessorAction
	{
		/// <summary>
		/// Retrieves the five most recent posts for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		List<Comment> GetRecentComments();

		/// <summary>
		/// Allows an authenticated user to create a comment.
		/// </summary>
		/// <param name="blogPostId">The blog post the comment is associated with.</param>
		/// <param name="username">The username that created the comment.</param>
		/// <param name="comment">The comment being created.</param>
		/// <returns></returns>
		void CreateComment(int blogPostId, string username, string comment);

		/// <summary>
		/// Retrieves all the comments for a given blog post.
		/// </summary>
		/// <param name="blogPostId">Id of the blog post the comments are associated with.</param>
		/// <returns></returns>
		List<Comment> GetCommentsForPost(int blogPostId);
	}
}
