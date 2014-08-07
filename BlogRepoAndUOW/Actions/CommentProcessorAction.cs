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

		/// <summary>
		/// Allows an authenticated user to create a comment.
		/// </summary>
		/// <param name="blogPostId">The blog post the comment is associated with.</param>
		/// <param name="username">The username that created the comment.</param>
		/// <param name="comment">The comment being created.</param>
		/// <returns></returns>
		public void CreateComment(int blogPostId, string username, string comment)
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					//Create the comment
					var commentObj = new Comment
					{
						Body = comment,
						CreatedOn = DateTime.Now
					};

					//Insert it into the repository
					var commentId = repo.Add(commentObj);

					//TODO: If this were a production application, I would add more code to validate that the blogPostId existed and that the comment was formatted in a suitable manner

					//Add the complex properties to it 
					var record = repo.FindComment(commentId);
					var author = repo.GrabUserProfile(a => a.Username == username).First();
					var post = repo.Find(blogPostId);
					record.Author = author;
					record.Post = post;

					//Save the record
					repo.InsertOrUpdate(record);
				}
			}
		}

		/// <summary>
		/// Retrieves all the comments for a given blog post.
		/// </summary>
		/// <param name="blogPostId">Id of the blog post the comments are associated with.</param>
		/// <returns></returns>
		public List<Comment> GetCommentsForPost(int blogPostId)
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var comments = repo.GrabComments(a => a.Post.Id == blogPostId).ToList();
					return comments;
				}
			}
		}
	}
}
