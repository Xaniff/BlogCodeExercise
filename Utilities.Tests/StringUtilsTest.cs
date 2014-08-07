using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
	[TestClass]
	public class StringUtilsTest
	{
		[TestMethod]
		public void OnlyTakeFirstParagraphOfMultilineHtmlText()
		{
			const string input = "\"This is an example description. It serves to inform the user about relevant information regarding the post they're looking at.\"<div><br></div><div>I don't entirely intend for this to contain much more text than this.</div>";
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 180, false);
			const string expectedResult = "\"This is an example description. It serves to inform the user about relevant information regarding the post they're looking at.\"";
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void OnlyTakeFirstParagraphOfMultilinePlainText()
		{
			const string input = "This is an example description. It serves to inform the user about relevant information regarding the post they're looking at.\nI don't entirely intend for this to contain much more text than this.";
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 180, false);
			const string expectedResult =
				"This is an example description. It serves to inform the user about relevant information regarding the post they're looking at.";
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void RetainWordsNoSpacesNoEllipses()
		{
			var input = new string('a', 20);
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 5, false);
			Assert.AreEqual(new string('a', 5), result);
		}

		[TestMethod]
		public void RetainWordsNoSpacesWithEllipses()
		{
			var input = new string('a', 20);
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 5);
			Assert.AreEqual(string.Format("{0}...", new string('a', 2)), result);
		}

		[TestMethod]
		public void RetainWordsAreShorterThanRequiredLengthWithEllipses()
		{
			var input = new string('a', 40);
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 50);
			Assert.AreEqual(input, result);
		}

		[TestMethod]
		public void RetainWordsAreShorterThanRequriedLengthWithoutEllipses()
		{
			var input = new string('a', 20);
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 50);
			Assert.AreEqual(input, result);
		}

		[TestMethod]
		public void RetainWordsWorksWithEllipses()
		{
			const string input = "The cat jumped over the windmill.";
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 10);
			Assert.AreEqual("The cat...", result);
		}

		[TestMethod]
		public void RetainWordsWorksWithoutEllipses()
		{
			const string input = "The cat jumped over the windmill.";
			var result = StringUtils.RetrieveMaxCharactersButKeepWholeWords(input, 10, false);
			Assert.AreEqual("The cat", result);
		}
	}
}
