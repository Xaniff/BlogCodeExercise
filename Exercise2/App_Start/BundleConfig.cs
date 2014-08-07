using System.Web;
using System.Web.Optimization;

namespace Exercise2
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			#region Javascript
			
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/standard").Include(
				"~/Scripts/modernizr-*",
				"~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
				"~/Scripts/jquery.signalR-2.1.1.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			var angularRouter = new Bundle("~/bundles/angularRouter").Include(
				"~/Scripts/angularjs/angular-ui-router.js");
			angularRouter.Transforms.Clear();
			bundles.Add(angularRouter);

			//Contains the scripts for the blog - intended for debug builds
			bundles.Add(new Bundle("~/bundles/blogDebug").IncludeDirectory("~/Scripts/blog/", "*.js", true));

			//Contains the scripts for the blog - intended for release builds as they're concatenated, uglified and minified
			bundles.Add(new Bundle("~/bundles/blogRelease").Include("~/Scripts/blog.min.js"));

			#endregion

			#region CSS/LESS

			bundles.Add(new Bundle("~/Content/css").Include(
				"~/Content/bootstrap.css",
				"~/Content/animate.css",
				"~/Content/fontawesome/font-awesome.css"));

			var lessBundle = new Bundle("~/Content/less-style").IncludeDirectory("~/Content/LESS/", "*.less");
			lessBundle.Transforms.Add(new LessTransform());
			//lessBundle.Transforms.Add(new CssMinify());
			bundles.Add(lessBundle);

			#endregion
			

			// Set EnableOptimizations to false for debugging. For more information,
			// visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
			BundleTable.EnableOptimizations = false;
#endif
#if RELEASE
			BundleTable.EnableOptimizations = true;
#endif
		}
	}
}
