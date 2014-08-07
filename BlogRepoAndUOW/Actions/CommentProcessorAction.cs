using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBoundedContext;
using DataLayer;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public class CommentProcessorAction : ICommentProcessorAction
	{
		/// <summary>
		/// Retrieves the five most recent posts for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		public List<Comment> GetRecentComments()
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var comments = repo.GrabAllComments().OrderByDescending(a => a.CreatedOn).Take(5).ToList();
					return comments;
				}	
			}
		}
	}
}
