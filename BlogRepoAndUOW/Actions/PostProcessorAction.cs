using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogBoundedContext;
using DataLayer;
using DomainClasses.Entities;

namespace BlogRepoAndUOW.Actions
{
	public class PostProcessorAction : IPostProcessorAction
	{
		/// <summary>
		/// Retrieves the five most recent posts for the recent post portion of the home page.
		/// </summary>
		/// <returns></returns>
		public List<BlogPost> GetRecentPosts()
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var posts = repo.GrabAll().OrderByDescending(a => a.CreatedOn).Take(5).ToList();
					return posts;
				}
			}
		}

		/// <summary>
		/// Retrieves all the posts in order of author name, then date descending, unless the 
		/// authorId is populated, then follow this order, but filter down to only that author.
		/// </summary>
		/// <param name="authorId">Optional: Id of the author to filter by.</param>
		/// <returns></returns>
		public List<BlogPost> GetPostsByAuthor(int? authorId)
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var posts = new List<BlogPost>();

					if (authorId == null)
						posts = repo.GrabAll().OrderBy(a => a.Author.Username).ThenByDescending(a => a.CreatedOn).ToList();
					else
						posts = repo.GrabAll().Where(a => a.Author.Id == authorId).OrderByDescending(a => a.CreatedOn).ToList();

					return posts;
				}
			}
		}

		/// <summary>
		/// Retrieves all the posts in order of date descending, then name.
		/// </summary>
		/// <returns></returns>
		public List<BlogPost> GetPostsByDate()
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var posts = repo.GrabAll().OrderByDescending(a => a.CreatedOn).ThenBy(a => a.Title).ToList();
					return posts;
				}
			}
		}

		/// <summary>
		/// Allows an authenticated user to create a blog post.
		/// </summary>
		/// <param name="post">The blog post to create.</param>
		/// <param name="username"></param>
		/// <returns>The id of the post so the client can route to it.</returns>
		public int CreatePost(BlogPost post, string username)
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					//Create a new post record - we'll associate it with the user in just a bit
					var postId = repo.Add(post);

					//Now retrieve it
					var postRecord = repo.Find(postId);

					//Retrieve the user information
					var userProfile = repo.GrabUserProfile(a => a.Username == username).FirstOrDefault();
					if (userProfile == null)
						return -1;

					//Update the post record properties to include the user information
					postRecord.Author = userProfile;

					//Now, update the post record
					repo.InsertOrUpdate(postRecord);
					return postId;
				}
			}
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
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					//Verify that the given username actually owns the post
					var posts = repo.Grab(a => a.Id == post.Id).Where(a => a.Author.Username == username);
					if (!posts.Any())
						return -1; //The username has no association;

					//There must be a post available at this point.
					var record = posts.FirstOrDefault();
					if (record == null)
						return -1; //This probably shouldn't happen.

					//Overwrite the properties of the current record with the new values
					record.Message = post.Message;
					record.Title = post.Title;

					//Update the record
					var postId = repo.InsertOrUpdate(record);
					return postId;
				}
			}
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
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					//Verify that the given username actually owns the post
					var posts = repo.Grab(a => a.Id == id).Where(a => a.Author.Username == username);
					if (!posts.Any())
						return; //The username has no association

					//There must be a post available at this point
					var post = posts.FirstOrDefault();
					if (post == null)
						return; //This probably shouldn't happen

					repo.Remove(post);
					//Done!
				}
			}
		}

		/// <summary>
		/// Retrieves a single post from the data store as identified by its Id value.
		/// </summary>
		/// <param name="postId">Identifier for the post.</param>
		/// <returns></returns>
		public BlogPost GetPostById(int postId)
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var post = repo.Grab(a => a.Id == postId).Include(a => a.Author).Include(a => a.Comments).FirstOrDefault();
					return post;
				}
			}
		}

		/// <summary>
		/// Retrieves all the current authors from the data store.
		/// </summary>
		/// <returns></returns>
		public List<UserProfile> GetAllAuthors()
		{
			using (var uow = new UnitOfWork<PostContext>())
			{
				using (var repo = new PostRepository(uow))
				{
					var authors = repo.GrabAllProfiles().OrderBy(a => a.Username).ToList();
					return authors;
				}
			}
		}

		
	}
}
