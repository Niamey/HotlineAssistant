using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Models;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IMessengerApiService
	{
		Task<SendingStatusApiModel> SendMessageAsync<TResult>(IMessagePackageApiModel<TResult> msg, CancellationToken cancellationToken);
		Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel messageStatusRequest, CancellationToken cancellationToken);
	}
}
