using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public static class StringUtils
	{
		/// <summary>
		/// Given a string, this will return a response that contains the characterCount
		/// or fewer characters, maintaining whole words.
		/// </summary>
		/// <param name="input">Original input.</param>
		/// <param name="characterCount">Number of characters to keep.</param>
		/// <param name="useEllipses">Should finish response with '...' if shortened.</param>
		/// <returns></returns>
		public static string RetrieveMaxCharactersButKeepWholeWords(string input, int characterCount = 100, bool useEllipses = true)
		{
			if (string.IsNullOrWhiteSpace(input))
				return string.Empty;

			//If there are any newline characters, we only want the text prior to the first newline portion.
			if (input.Contains("\n") || input.Contains("<"))
			{
				if (input.Contains("\n"))
				{
					var split = input.Split('\n');
					input = split[0].ToString(CultureInfo.InvariantCulture);
				}

				if (input.Contains("<"))
				{
					var split = input.Split('<');
					input = split[0].ToString(CultureInfo.InvariantCulture);
				}
			}

			if (input.Length <= characterCount)
				return input;

			//If we need the ellipses, make room for it
			if (useEllipses)
				characterCount -= 3;

			//Capture the length up to the maximum character count
			var result = input.Substring(0, characterCount + 1); //See the +1 with the related RetrainWordsWorksWithEllipses test

			//If there are no spaces, simply return the substring, perhaps with the ellipses
			if (!result.Contains(" "))
			{
				return useEllipses ? string.Format("{0}...", result.Substring(0, characterCount)) : result.Substring(0, characterCount);
			}

			//Iterate backwards through the string to find the index of the last space
			for (var ii = result.Length - 1; ii > 0; ii--)
			{
				if (result[ii] == ' ')
				{
					//Recapture the substring to this value
					var newResult = input.Substring(0, ii);

					//Determine whether to add ellipses or not to result
					return useEllipses ? string.Format("{0}...", newResult) : newResult;
				}
			}

			//We shouldn't ever get to this point
			return input.Substring(0, characterCount);
		}
	}
}
