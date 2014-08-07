using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Exercise2
{
	[HubName("update")]
	public class UpdateHub : Hub
	{
		public void SendUpdatePostMessage()
		{
			Clients.All.updatePosts();
		}
	}
}