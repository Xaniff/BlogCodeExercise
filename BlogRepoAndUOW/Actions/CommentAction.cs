using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public class CommentAction
	{
		private ICommentProcessorAction _processorAction;

		public CommentAction(ICommentProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Retrieves the five most recent posts for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		public List<Comment> GetRecentComments()
		{
			var result = _processorAction.GetRecentComments();
			return result;
		}

		/// <summary>
		/// Retrieves all the comments for a given blog post.
		/// </summary>
		/// <param name="blogPostId">Id of the blog post the comments are associated with.</param>
		/// <returns></returns>
		public List<Comment> GetCommentsForPost(int blogPostId)
		{
			var result = _processorAction.GetCommentsForPost(blogPostId);
			return result;
		}

		/// <summary>
		/// Allows an authenticated user to create a comment.
		/// </summary>
		/// <param name="blogPostId">The blog post the comment is associated with.</param>
		/// <param name="username">The username that created the comment.</param>
		/// <param name="comment">The comment being created.</param>
		/// <returns></returns>
		public void CreateComment(int blogPostId, string username, string comment)
		{
			_processorAction.CreateComment(blogPostId, username, comment);
		}
	}
}
