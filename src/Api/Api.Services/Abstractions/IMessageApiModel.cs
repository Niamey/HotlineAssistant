using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IMessageApiModel 
	{
		MessageTypeEnum MessageType { get; }
		object GetMessage();
	}

	public interface IMessageApiModel<TResult> : IMessageApiModel
	{
		string Message { get; set; }
		new TResult GetMessage();

		object IMessageApiModel.GetMessage()
		{
			return GetMessage();
		}
	}
}
