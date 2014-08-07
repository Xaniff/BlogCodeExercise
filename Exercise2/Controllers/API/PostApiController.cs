using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using BlogRepoAndUOW.Actions;
using DomainClasses.Entities;
using Microsoft.AspNet.SignalR;
using UserRepoAndUOW.Actions;
using Utilities;

namespace Exercise2.Controllers.API
{
	public class PostApiController : ApiController
	{
		/// <summary>
		/// Retrieves all the posts in order of date descending, then title.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetPostsByDate()
		{
			var postAction = new PostAction(new PostProcessorAction());
			//Retrieve the posts from the data store
			var posts = postAction.GetPostsByDate(); 
			
			//Limit the body length of each of the posts to 500 characters
			foreach (var post in posts)
			{
				post.Message = StringUtils.RetrieveMaxCharactersButKeepWholeWords(post.Message, 500);
			}

			//Create a new anonymous list for storing all the data
			var list = new[]
			{
				new
				{
					Id = "",
					Title = "",
					Body = "",
					Date = "",
					Author = ""
				}
			}.ToList();
			list.Clear();
			foreach (var post in posts)
			{
				list.Add(new
				{
					Id = post.Id.ToString(CultureInfo.InvariantCulture),
					Title = post.Title,
					Body = post.Message,
					Date = post.CreatedOn.ToString("O"),
					Author = post.Author.Username
				});
			}

			return Request.CreateResponse(HttpStatusCode.OK, list);
		}

		/// <summary>
		/// Retrieves all the posts in order of author name, then date descending, unless the 
		/// authorId is populated, then follow this order, but filter down to only that author.
		/// </summary>
		/// <param name="authorId">Id of the author to filter by.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetPostsByAuthor(int authorId)
		{
			var postAction = new PostAction(new PostProcessorAction());
			//Retrieve the posts from the data store
			var posts = postAction.GetPostsByAuthor(authorId);

			//Limit the body length of each of the posts to 500 characters
			foreach (var post in posts)
			{
				post.Message = StringUtils.RetrieveMaxCharactersButKeepWholeWords(post.Message, 500);
			}

			//Create a new anonymous list for storing all the data
			var list = new[]
			{
				new
				{
					Id = "",
					Title = "",
					Body = "",
					Date = "",
					Author = ""
				}
			}.ToList();
			list.Clear();
			foreach (var post in posts)
			{
				list.Add(new
				{
					Id = post.Id.ToString(CultureInfo.InvariantCulture),
					Title = post.Title,
					Body = post.Message,
					Date = post.CreatedOn.ToString("O"),
					Author = post.Author.Username
				});
			}

			return Request.CreateResponse(HttpStatusCode.OK, list);
		}

		/// <summary>
		/// Retrieves a single post from the data store as identified by its Id value.
		/// </summary>
		/// <param name="postId">Identifier for the post.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetPostById(int postId)
		{
			var postAction = new PostAction(new PostProcessorAction());

			//Retrieve the post from the data store
			var post = postAction.GetPostById(postId);

			//Strip the HTML tags from the body
			//var regex = new Regex("<.*?>", RegexOptions.Compiled);
			//post.Message = regex.Replace(post.Message, string.Empty);

			return Request.CreateResponse(HttpStatusCode.OK, new {id = post.Id.ToString(CultureInfo.InvariantCulture), title = post.Title, message = post.Message, date = post.CreatedOn.ToString("O"), author = post.Author.Username, authorId = post.Author.Id});
		}

		/// <summary>
		/// Retrieves all the current authors from the data store.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetAllAuthors()
		{
			var postAction = new PostAction(new PostProcessorAction());

			//Retrieve the authors
			var authors = postAction.GetAllAuthors();

			var list = new[]
			{
				new
				{
					Id = 0,
					Username = "",
					PostCount = 0
				}
			}.ToList();
			list.Clear();
			foreach (var author in authors)
			{
				list.Add(new
				{
					Id = author.Id,
					Username = author.Username,
					PostCount = author.BlogPosts.Count
				});
			}

			return Request.CreateResponse(HttpStatusCode.OK, list);
		}

		/// <summary>
		/// Retrieves the five most recent posts for the recent posts portion of the home page.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetRecentPosts()
		{
			var postAction = new PostAction(new PostProcessorAction());

			//Retrieve the posts from the data store
			var posts = postAction.GetRecentPosts();

			foreach (var post in posts)
			{
				//Strip the HTML tags from the body
				var regex = new Regex("<.*?>", RegexOptions.Compiled);
				post.Message = regex.Replace(post.Message, string.Empty);

				//Limit the body length of each of the posts to 100 characters
				post.Message = StringUtils.RetrieveMaxCharactersButKeepWholeWords(post.Message);
			}

			//Create a new anonymous list for storing all the data
			var list = new[]
			{
				new
				{
					Id = "",
					Title = "",
					Body = "",
					Date = "",
					Author = ""
				}
			}.ToList();
			list.Clear();
			foreach (var post in posts)
			{
				list.Add(new
				{
					Id = post.Id.ToString(CultureInfo.InvariantCulture),
					Title = post.Title,
					Body = post.Message,
					Date = post.CreatedOn.ToString("O"),
					Author = post.Author.Username
				});
			}

			return Request.CreateResponse(HttpStatusCode.OK, list);
		}

		/// <summary>
		/// Retrieves the five most recent comments for the recent comment portion of the home page.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage GetRecentComments()
		{
			var commentAction = new CommentAction(new CommentProcessorAction());

			//Retrieve the comments from the data store
			var comments = commentAction.GetRecentComments();

			//Limit the body length of each of the comments to 100 characters
			foreach (var comment in comments)
			{
				comment.Body = StringUtils.RetrieveMaxCharactersButKeepWholeWords(comment.Body);
			}

			//Create a new anonymous list for storing all the data
			var list = new[]
			{
				new
				{
					Body = "",
					Author = "",
					Date = ""
				}
			}.ToList();
			list.Clear();
			foreach (var comment in comments)
			{
				list.Add(new
				{
					Body = comment.Body,
					Author = comment.Author.Username,
					Date = comment.CreatedOn.ToString("O")
				});
			}

			return Request.CreateResponse(HttpStatusCode.OK, list);
		}

		/// <summary>
		/// Deletes a blog post from the data store.
		/// </summary>
		/// <param name="postId">Id of the blog post to delete.</param>
		/// <returns></returns>
		[HttpGet]
		public HttpResponseMessage BlogItemDeletion(int postId)
		{
			var tokenAction = new TokenAction(new TokenProcessorAction());

			//Validate the header token
			var authResult = tokenAction.ValidateToken(HttpContext.Current);
			if (authResult != HttpStatusCode.OK)
				return Request.CreateResponse(authResult);

			//Extract the user
			var identity = tokenAction.ExtractCustomCookiePrincipal(HttpContext.Current);
			if (identity == null)
				return Request.CreateResponse(HttpStatusCode.InternalServerError);
			var username = identity.Username;

			//Delete the specified post for the specified user.
			var postAction = new PostAction(new PostProcessorAction());
			postAction.DeletePost(postId, username);

			//Send the message to the clients to update their posts
			var hub = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();
			hub.Clients.All.updatePosts();

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		/// <summary>
		/// Allows an authenticated user to create a blog post.
		/// </summary>
		/// <param name="post">The blog post to create.</param>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage CreatePost(BlogPost post)
		{
			var tokenAction = new TokenAction(new TokenProcessorAction());

			//Validate the header token
			var authResult = tokenAction.ValidateToken(HttpContext.Current);
			if (authResult != HttpStatusCode.OK)
				return Request.CreateResponse(authResult);

			//Extract the user
			var identity = tokenAction.ExtractCustomCookiePrincipal(HttpContext.Current);
			if (identity == null)
				return Request.CreateResponse(HttpStatusCode.InternalServerError);
			var username = identity.Username;

			//Create the post for the specified user
			var postAction = new PostAction(new PostProcessorAction());
			var postId = postAction.CreatePost(post, username);
			if (postId == -1)
				return Request.CreateResponse(HttpStatusCode.NotAcceptable);

			//Send the message to the clients to update their posts
			var hub = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();
			hub.Clients.All.updatePosts();

			return Request.CreateResponse(HttpStatusCode.OK, new {postId = postId});
		}

		/// <summary>
		/// Allows the author to update the blog post they wrote.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage EditPost(BlogPost post)
		{
			var tokenAction = new TokenAction(new TokenProcessorAction());

			//Validate the header token
			var authResult = tokenAction.ValidateToken(HttpContext.Current);
			if (authResult != HttpStatusCode.OK)
				return Request.CreateResponse(authResult);

			//Extract the user
			var identity = tokenAction.ExtractCustomCookiePrincipal(HttpContext.Current);
			if (identity == null)
				return Request.CreateResponse(HttpStatusCode.InternalServerError);
			var username = identity.Username;

			//Update the post for the specified user
			var postAction = new PostAction(new PostProcessorAction());
			var postId = postAction.UpdatePost(post, username);
			if (postId == -1)
				return Request.CreateResponse(HttpStatusCode.NotAcceptable);

			//Send the message to the clients to update their posts
			var hub = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();
			hub.Clients.All.updatePosts();

			return Request.CreateResponse(HttpStatusCode.OK, new {postId = postId});
		}
	}
}