using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class JsonHelper
	{
		public static string ToJson(object data, Formatting formatter = Formatting.Indented)
		{
			Assure.ArgumentNotNull(data, nameof(data));

			return JsonConvert.SerializeObject(data, formatter);
		}

		public static T FromJson<T>(string json)
		{
			Assure.ArgumentNotNullNotEmpty(json, nameof(json));

			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
