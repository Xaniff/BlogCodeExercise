using System.Web.Optimization;

namespace Exercise2
{
	public class LessTransform : IBundleTransform
	{
		public void Process(BundleContext context, BundleResponse response)
		{
			response.Content = dotless.Core.Less.Parse(response.Content);
			response.Content = "text/css";
		}
	}
}