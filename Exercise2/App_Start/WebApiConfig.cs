using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Exercise2
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//Web API configuration and services
			var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			//Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "AuthenticationApi",
				routeTemplate: "api/auth/{action}",
				defaults: new {controller = "AuthenticationApi"});

			config.Routes.MapHttpRoute(
				name: "PostApi",
				routeTemplate: "api/posts/{action}",
				defaults: new {controller = "PostApi"});
			
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
				);
		}
	}
}