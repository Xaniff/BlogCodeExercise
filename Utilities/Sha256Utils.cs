using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public static class Sha256Utils
	{
		/// <summary>
		/// Generates a random string to provide a salting hash value.
		/// </summary>
		/// <param name="length">The length of the resulting string.</param>
		/// <param name="canContainSymbols">Whether the generated string can possibly contain symbols.</param>
		/// <returns>A string comprised of random characters.</returns>
		public static string GenerateRandomString(int length, bool canContainSymbols = false)
		{
			const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
			const string symbols = @"-=_+[]{};':,.<>?!@#$%^&*()";
			var chars = canContainSymbols ? string.Format("{0}{1}", characters, symbols).ToCharArray() : characters.ToCharArray();
			var data = new byte[1];
			var crypto = new RNGCryptoServiceProvider();
			crypto.GetNonZeroBytes(data);
			data = new byte[length];
			crypto.GetNonZeroBytes(data);
			var result = new StringBuilder(length);
			foreach (var b in data)
			{
				result.Append(chars[b % chars.Length]);
			}
			return result.ToString();
		}

		/// <summary>
		/// This function hashes the input five times for somewhat increased security
		/// of the values within the data store.
		/// </summary>
		/// <param name="password">The password value getting hashed.</param>
		/// <param name="salt">The salt value being used.</param>
		/// <returns>The hashed value of the input and salt.</returns>
		public static string GetSuperHash(string password, string salt)
		{
			//Put the salt into the input string
			var saltedPassword = GetSaltedValue(password, salt);

			//Create the output
			var output = "";
			for (var ii = 0; ii < 5; ii++)
			{
				output = GetSha256Hash(saltedPassword);
			}
			return output;
		}

		/// <summary>
		/// Salts an input string with a salt value. This uses a consistent function to
		/// combine the input with the salt value to stretch the key, and in a way that's slightly more 
		/// sophisticated than merely appending the salt value to the end of the password value. The intent 
		/// of this is to make it far  more difficult for a malicious user to determine the values of each user's password
		/// from the hashes in the database.
		/// </summary>
		/// <param name="input">Input value, typically a password.</param>
		/// <param name="salt">The salt value being injected into the input.</param>
		/// <returns>Return the salted value having been integrated into the input value.</returns>
		private static string GetSaltedValue(string input, string salt)
		{
			if (input.Length > 0)
				return (input.Substring(0, 1) + salt.Substring(0, 5) + input.Substring(1) + salt.Substring(5));
			return salt;
		}

		/// <summary>
		/// Yields the SHA-256 hash for a given input value.
		/// </summary>
		/// <param name="input">Input string.</param>
		/// <returns>The output hash value.</returns>
		public static string GetSha256Hash(string input)
		{
			//Create an instance of the SHA256 object.
			var sha256 = SHA256.Create();

			//Get the bytes for the input string
			var bytes = Encoding.UTF8.GetBytes(input);

			//Convert the input string to a byte array
			var data = sha256.ComputeHash(bytes);

			//Create a new StringBuilder to collect the bytes and create a string
			var stringBuilder = new StringBuilder();

			//Loop through each byte of the hashed data and format
			//each one as a hexidecimal string
			for (var ii = 0; ii < data.Length; ii++)
			{
				stringBuilder.Append(data[ii].ToString("x2"));
			}

			//Return the hexidecimal string
			return stringBuilder.ToString();
		}
	}
}
