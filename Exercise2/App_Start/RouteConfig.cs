using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Exercise2
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			
			routes.MapRoute(
				name: "Home",
				url: "",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "Login",
				url: "login",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "Register",
				url: "register",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "ViewPost",
				url: "view{id}",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "NewPost",
				url: "new",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "EditPost",
				url: "edit{id}",
				defaults: new {controller = "Home", action = "Index"});

			routes.MapRoute(
				name: "NotFound",
				url: "{*url}",
				defaults: new {controller = "Home", action = "Index", id = ""});
		}
	}
}
