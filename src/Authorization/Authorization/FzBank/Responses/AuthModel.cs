using Newtonsoft.Json;

namespace Vostok.Hotline.Authorization.FzBank.Responses
{
	public class AuthModel
	{
		[JsonProperty("session")]
		public SessionDataResponseModel Session { get; set; }

		[JsonProperty("errorCode")]
		public int ErrorCode { get; set; }

		[JsonProperty("errorString")]
		public string ErrorString { get; set; }
	}
}
