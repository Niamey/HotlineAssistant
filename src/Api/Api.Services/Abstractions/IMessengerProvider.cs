using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	internal interface IMessengerProvider
	{
		public ProviderTypeEnum ProviderType { get; }
		Task<SendingStatusApiModel> SendAsync<TResult>(IMessagePackageApiModel<TResult> msg, CancellationToken cancellationToken);
		Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel msg, CancellationToken cancellationToken);
	}
}
