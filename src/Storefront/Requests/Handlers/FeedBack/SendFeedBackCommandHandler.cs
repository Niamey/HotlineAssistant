using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Storefront.Requests.Commands;
using Vostok.Hotline.Storefront.Services.FeedBack;

namespace Vostok.Hotline.Storefront.Requests.Handlers.FeedBack
{
	public class SendFeedBackCommandHandler : IRequestHandler<SendFeedBackCommand, StatusCommandViewModel>
	{
		private readonly FeedBackService _feedBackService;

		public SendFeedBackCommandHandler(FeedBackService feedBackService) 
		{
			_feedBackService = feedBackService;
		}

		public async Task<StatusCommandViewModel> Handle(SendFeedBackCommand request, CancellationToken cancellationToken)
		{
			return await _feedBackService.SendAsync(request, cancellationToken);
		}
	}
}
