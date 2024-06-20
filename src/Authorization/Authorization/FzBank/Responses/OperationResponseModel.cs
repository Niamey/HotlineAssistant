using Newtonsoft.Json;

namespace Vostok.Hotline.Authorization.FzBank.Responses
{
	public class OperationResponseModel
	{
		[JsonProperty("operationName")]
		public string OperationName { get; set; }
		[JsonProperty("moduleName")]
		public string ModuleName { get; set; }
	}
}
