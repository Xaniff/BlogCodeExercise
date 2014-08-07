using System.Web.Mvc;
using Exercise2.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void IndexExists()
		{
			var controller = new HomeController();
			var result = controller.Index() as ViewResult;
			Assert.IsNotNull(result);
		}
	}
}
