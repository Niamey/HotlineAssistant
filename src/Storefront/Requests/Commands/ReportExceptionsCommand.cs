using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Storefront.Models.FeedBack;

namespace Vostok.Hotline.Storefront.Requests.Commands
{
	public class ReportExceptionsCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public ReportedError[] Exceptions { get; set; }

		[BindRequired]
		public string RequestedUrl { get; set; }

	}
}
