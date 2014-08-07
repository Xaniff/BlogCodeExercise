using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public class PostAction
	{
		private IPostProcessorAction _processorAction;

		public PostAction(IPostProcessorAction processorAction)
		{
			_processorAction = processorAction;
		}

		/// <summary>
		/// Retrieves the five most recent posts for the recent post portion of the home page.
		/// </summary>
		/// <returns></returns>
		public List<BlogPost> GetRecentPosts()
		{
			var result = _processorAction.GetRecentPosts();
			return result;
		}

		/// <summary>
		/// Retrieves all the posts in order of date descending, then name.
		/// </summary>
		/// <returns></returns>
		public List<BlogPost> GetPostsByDate()
		{
			var result = _processorAction.GetPostsByDate();
			return result;
		}

		/// <summary>
		/// Retrieves all the posts in order of author name, then date descending, unless the 
		/// authorId is populated, then follow this order, but filter down to only that author.
		/// </summary>
		/// <param name="authorId">Id of the author to filter by.</param>
		/// <returns></returns>
		public List<BlogPost> GetPostsByAuthor(int authorId)
		{
			var result = _processorAction.GetPostsByAuthor(authorId);
			return result;
		}

		/// <summary>
		/// Allows an authenticated user to create a blog post.
		/// </summary>
		/// <param name="post">The blog post to create.</param>
		/// <param name="username"></param>
		/// <returns>The id of the post so the client can route to it.</returns>
		public int CreatePost(BlogPost post, string username)
		{
			var result = _processorAction.CreatePost(post, username);
			return result;
		}

		/// <summary>
		/// Allows the author to update a blog post they wrote.
		/// </summary>
		/// <remarks>
		/// As per the requirements, updating a post should only be allowed by the User that made it.
		/// </remarks>
		/// <param name="post">The blog post to update, including the Id to identify it.</param>
		/// <param name="username"></param>
		/// <returns>The id of the post, so the client can route to it.</returns>
		public int UpdatePost(BlogPost post, string username)
		{
			var result = _processorAction.UpdatePost(post, username);
			return result;
		}

		/// <summary>
		/// Deletes a blog post from the data store.
		/// </summary>
		/// <remarks>
		/// As per the requirements, deleting a post should only be allowed by the User that made it.
		/// </remarks>
		/// <param name="id">Id of the blog post to delete</param>
		/// <param name="username"></param>
		public void DeletePost(int id, string username)
		{
			_processorAction.DeletePost(id, username);
		}

		/// <summary>
		/// Retrieves a single post from the data store as identified by its Id value.
		/// </summary>
		/// <param name="postId">Identifier for the post.</param>
		/// <returns></returns>
		public BlogPost GetPostById(int postId)
		{
			var result = _processorAction.GetPostById(postId);
			return result;
		}

		/// <summary>
		/// Retrieves all the current authors from the data store.
		/// </summary>
		/// <returns></returns>
		public List<UserProfile> GetAllAuthors()
		{
			var result = _processorAction.GetAllAuthors();
			return result;
		}
	}
}
