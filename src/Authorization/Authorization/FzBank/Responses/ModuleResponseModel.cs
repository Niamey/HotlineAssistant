using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vostok.Hotline.Authorization.FzBank.Responses
{
	public class ModuleResponseModel
	{
		[JsonProperty("moduleName")]
		public string ModuleName { get; set; }

		[JsonProperty("moduleType")]
		public string ModuleType { get; set; }

		[JsonProperty("moduleParams")]
		public List<ModuleParameterResponseModel> ModuleParams { get; set; }

		[JsonProperty("isActive")]
		public bool IsActive { get; set; }
	}
}
