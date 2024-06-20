using System;
using Newtonsoft.Json;

namespace Vostok.Hotline.Authorization.FzBank.Models
{
	public class FzBankAuthorizationDataApiModel
	{
		//[JsonProperty("modules")]
		//public List<Module> Modules { get; set; }
		//[JsonProperty("operations")]
		//public List<Operation> Operations { get; set; }

		[JsonProperty("sid")]
		public Guid UserSessionId { get; set; }

		[JsonProperty("login")]
		public string Login { get; set; }

		[JsonProperty("fio")]
		public string FIO { get; set; }

		public string Position { get; set; }

		public string AvatarBase64 { get; set; }
	}
}
