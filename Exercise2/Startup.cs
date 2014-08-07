using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercise2.Startup))]
namespace Exercise2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
	        var config = new HubConfiguration {EnableJSONP = true};
	        app.MapSignalR(config);
        }
    }
}
