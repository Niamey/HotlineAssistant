using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Core.Common.Models.Responses.Commands
{
	public class StatusCommandViewModel: ResponseBaseModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}
