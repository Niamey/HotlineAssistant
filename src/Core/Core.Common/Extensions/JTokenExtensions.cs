using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Vostok.Hotline.Core.Common.Extensions
{
	public static class JTokenExtensions
	{
		// Recursively converts a JObject with PascalCase names to camelCase
		[Pure]
		public static JObject ToCamelCase(this JObject original)
		{
			var newObj = new JObject();
			foreach (var property in original.Properties())
			{
				var newPropertyName = property.Name.ToCamelCaseString();
				newObj[newPropertyName] = property.Value.ToCamelCaseJToken();
			}

			return newObj;
		}

		[Pure]
		public static JArray ToCamelCase(this JArray original)
		{
			return new JArray((original).Select(x => x.ToCamelCaseJToken()));
		}

		// Recursively converts a JToken with PascalCase names to camelCase
		[Pure]
		public static JToken ToCamelCaseJToken(this JToken original)
		{
			switch (original.Type)
			{
				case JTokenType.Object:
					return ((JObject)original).ToCamelCase();
				case JTokenType.Array:
					return new JArray(((JArray)original).Select(x => x.ToCamelCaseJToken()));
				default:
					return original.DeepClone();
			}
		}

		// Convert a string to camelCase
		[Pure]
		public static string ToCamelCaseString(this string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return char.ToLowerInvariant(str[0]) + str.Substring(1);
			}

			return str;
		}
	}
}
