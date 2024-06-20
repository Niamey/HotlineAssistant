using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Storefront.Requests.Commands;
using Vostok.Hotline.Storefront.Services.FeedBack;

namespace Vostok.Hotline.Storefront.Requests.Handlers.FeedBack
{
	public class ReportExceptionsCommandHandler : IRequestHandler<ReportExceptionsCommand, StatusCommandViewModel>
	{
		private readonly FeedBackService _feedBackService;

		public ReportExceptionsCommandHandler(FeedBackService feedBackService)
		{
			_feedBackService = feedBackService;
		}
		public async Task<StatusCommandViewModel> Handle(ReportExceptionsCommand request, CancellationToken cancellationToken)
		{
			return await _feedBackService.ReportExceptionsAsync(request, cancellationToken);
		}
	}
}
