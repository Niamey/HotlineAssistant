using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vostok.Hotline.Authorization.FzBank.Responses
{
	public class SessionDataResponseModel
	{
		[JsonProperty("modules")]
		public List<ModuleResponseModel> Modules { get; set; }
		[JsonProperty("operations")]
		public List<OperationResponseModel> Operations { get; set; }

		[JsonProperty("sid")]
		public Guid SessionId { get; set; }

		[JsonProperty("login")]
		public string Login { get; set; }

		[JsonProperty("fio")]
		public string FIO { get; set; }

		public string Position { get; set; }

		public string AvatarBase64 { get; set; }
	}
}
