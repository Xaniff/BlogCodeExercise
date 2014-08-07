using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utilities
{
	public class JsonUtils<T>
	{
		public static T MapFromJson(string json, string parentToken = null)
		{
			var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();
			return JsonConvert.DeserializeObject<T>(jsonToParse);
		}
	}
}
