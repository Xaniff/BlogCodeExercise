using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using BlogRepoAndUOW.Actions;
using Utilities;

namespace Exercise2.Controllers
{
	public class RssController : Controller
	{
		[HttpGet]
		public ActionResult Posts()
		{
			Response.ContentType = "application/rss+xml";
			var postAction = new PostAction(new PostProcessorAction());

			//Retrieve the port of the current request to use later
			var url = HttpContext.Request.Url;
			int? port = null;
			if (url != null)
				port = url.Port;
			var baseUrl = "http://localhost:" + port;

			//Retrieve the posts from the data store
			var posts = postAction.GetPostsByDate();
			var sb = new StringBuilder();
			sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			sb.AppendLine("<rss version\"2.0\">");
			sb.AppendLine("<channel>");
			sb.AppendLine("<title>My Blog Posts</title>");
			sb.AppendLine(
				"<description>This channel will list all of the posts chronologically in a date-descending order.</description>");
			sb.AppendFormat("<link>{0}</link>", baseUrl);
			sb.AppendFormat("<lastBuildDate>{0}</lastBuildDate>", DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz"));
			sb.AppendLine("<ttl>1800</ttl>");
			foreach (var post in posts)
			{
				//Strip out the HTML tags from the body
				var regex = new Regex("<.*?>", RegexOptions.Compiled);
				post.Message = regex.Replace(post.Message, string.Empty);
				//Keep only the first 100 characters
				if (post.Message.Length > 100)
				{
					post.Message = StringUtils.RetrieveMaxCharactersButKeepWholeWords(post.Message, 100);
				}

				sb.AppendLine("<item>");
				sb.AppendFormat("<title>{0}</title>", post.Title);
				sb.AppendFormat("<description>{0}</description>", post.Message);
				sb.AppendFormat("<link>{0}/view?id={1}</link>", baseUrl, post.Id);
				sb.AppendFormat("<pubDate>{0}</pubDate>", post.CreatedOn.ToString("ddd, dd MMM yyyy HH:mm:ss zzz"));
				sb.AppendLine("</item>");
			}
			sb.AppendLine("</channel>");
			sb.AppendLine("</rss>");
			var rssOutput = sb.ToString();

			return Content(rssOutput);
		}
	}
}