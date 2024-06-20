using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Models;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class MessengerApiManager
	{
		private readonly IMessengerApiService _messengerApiService;

		public MessengerApiManager(IMessengerApiService messengerApiService)
		{
			_messengerApiService = messengerApiService;
		}

		public async Task<SendingStatusApiModel> SendMessageAsync<TResult>(IMessagePackageApiModel<TResult> msg, CancellationToken cancellationToken)
		{
			var result = await _messengerApiService.SendMessageAsync(msg, cancellationToken);
			return result;
		}

		public async Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel messageStatusRequest, CancellationToken cancellationToken)
		{
			var result = await _messengerApiService.GetStatusAsync(messageStatusRequest, cancellationToken);
			return result;
		}
	}
}
