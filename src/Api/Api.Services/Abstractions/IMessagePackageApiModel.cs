using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IMessagePackageApiModel<TResult>
	{
		public ProviderTypeEnum ProviderType { get; }
		public TResult GetMessage();
	}
}
