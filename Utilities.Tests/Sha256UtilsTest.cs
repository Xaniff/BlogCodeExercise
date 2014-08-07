using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
	[TestClass]
	public class Sha256UtilsTest
	{
		[TestMethod]
		public void CheckThatTheRandomStringLengthMatchesRequestedLength()
		{
			const int count = 25;
			var result = Sha256Utils.GenerateRandomString(count);

			Assert.AreEqual(count, result.Length);
		}

		[TestMethod]
		public void ShaHashShouldAlwaysYieldSameInputForSameInput()
		{
			var inputValue = Sha256Utils.GenerateRandomString(100);

			var result1 = Sha256Utils.GetSha256Hash(inputValue);
			var result2 = Sha256Utils.GetSha256Hash(inputValue);

			Assert.AreEqual(result1, result2);
		}
	}
}
