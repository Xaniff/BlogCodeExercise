using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public interface IPostProcessorAction
	{
		/// <summary>
		/// Retrieves the five most recent posts for the recent post portion of the home page.
		/// </summary>
		/// <returns></returns>
		List<BlogPost> GetRecentPosts();

		/// <summary>
		/// Retrieves all the posts in order of author name, then date descending, unless the 
		/// authorId is populated, then follow this order, but filter down to only that author.
		/// </summary>
		/// <param name="authorId">Optional: Id of the author to filter by.</param>
		/// <returns></returns>
		List<BlogPost> GetPostsByAuthor(int? authorId);

		/// <summary>
		/// Retrieves all the posts in order of date descending, then name.
		/// </summary>
		/// <returns></returns>
		List<BlogPost> GetPostsByDate();

		/// <summary>
		/// Allows an authenticated user to create a blog post.
		/// </summary>
		/// <param name="post">The blog post to create.</param>
		/// <param name="username"></param>
		/// <returns>The id of the post so the client can route to it.</returns>
		int CreatePost(BlogPost post, string username);

		/// <summary>
		/// Allows the author to update a blog post they wrote.
		/// </summary>
		/// <remarks>
		/// As per the requirements, updating a post should only be allowed by the User that made it.
		/// </remarks>
		/// <param name="post">The blog post to update, including the Id to identify it.</param>
		/// <param name="username"></param>
		/// <returns>The id of the post, so the client can route to it.</returns>
		int UpdatePost(BlogPost post, string username);

		/// <summary>
		/// Deletes a blog post from the data store.
		/// </summary>
		/// <remarks>
		/// As per the requirements, deleting a post should only be allowed by the User that made it.
		/// </remarks>
		/// <param name="id">Id of the blog post to delete</param>
		/// <param name="username"></param>
		void DeletePost(int id, string username);

		/// <summary>
		/// Retrieves a single post from the data store as identified by its Id value.
		/// </summary>
		/// <param name="postId">Identifier for the post.</param>
		/// <returns></returns>
		BlogPost GetPostById(int postId);

		/// <summary>
		/// Retrieves all the current authors from the data store.
		/// </summary>
		/// <returns></returns>
		List<UserProfile> GetAllAuthors();
	}
}
