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
	        app.MapSignalR();
        }
    }
}
