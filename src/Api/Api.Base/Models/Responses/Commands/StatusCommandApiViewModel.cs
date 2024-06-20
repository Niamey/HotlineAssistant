using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Base.Models.Responses.Commands
{
    public class StatusCommandApiViewModel: ResponseBaseModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}
