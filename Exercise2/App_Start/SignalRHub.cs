using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Exercise2
{
	[HubName("update")]
	public class SignalRHub : Hub
	{
		/// <summary>
		/// Used to indicate that a post was created or deleted.
		/// </summary>
		public static void NotifyCreateOrDeletePost()
		{
			var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
			context.Clients.All.ReceiveNotifyCreateOrDeletePost();
		}

		/// <summary>
		/// Used to indicate that a post was edited.
		/// </summary>
		public static void NotifyUpdatePosts(int postId)
		{
			var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
			context.Clients.All.ReceiveNotifyUpdatePosts(postId);
		}

		/// <summary>
		/// Used to indicate that a comment was created.
		/// </summary>
		public static void NotifyAddComments(int postId)
		{
			var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
			context.Clients.All.ReceiveNotifyAddComments(postId);
		}

		/// <summary>
		/// Used to indicate that an author was created.
		/// </summary>
		public static void NotifyAddAuthor()
		{
			var context = GlobalHost.ConnectionManager.GetHubContext<SignalRHub>();
			context.Clients.All.ReceiveNotifyAddAuthor();
		}
	}
}