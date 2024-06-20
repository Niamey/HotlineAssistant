using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Services.Models.Statement
{
	public class AuthLoginApiModel : ResponseBaseModel
	{
		public string JSessionId { get; set; }
		public string XSRFToken { get; set; }
	}
}
