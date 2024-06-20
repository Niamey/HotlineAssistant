using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Api.Services.Responses.Messenger;
using Vostok.Hotline.Api.Services.Responses.Messenger.Enums;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Mapper.Messenger
{
	internal static class SendingStatusMapperExtensions
	{
		public static SendingStatusApiModel ToSendingStatusApiModel(this MessageResponseModel response)
		{
			var result = new SendingStatusApiModel
			{
				ExternalId = response.ExternalId,
				MessagePackageId = response.MessageId,
				Status = new MessageStatusApiModel()
				{
					Status = response.Status.StatusId.ToMessageStatusEnum(),
					StatusTime = response.Status.StatusTime
				},
				PackageStatuses = new MessagePackageStatusesApiModel()
			};

			if (response.Statuses != null)
			{
				if (response.Statuses.Sms != null)
				{
					result.PackageStatuses.Add(
						new MessagePackageStatusApiModel()
						{
							MessageType = MessageTypeEnum.SMS,
							Status = response.Statuses.Sms.StatusId.ToMessageStatusEnum(),
							StatusTime = response.Statuses.Sms.StatusTime
						}
					);
				}
				if (response.Statuses.Viber != null)
				{
					result.PackageStatuses.Add(
						new MessagePackageStatusApiModel()
						{
							MessageType = MessageTypeEnum.Viber,
							Status = response.Statuses.Viber.StatusId.ToMessageStatusEnum(),
							StatusTime = response.Statuses.Viber.StatusTime
						}
					);
				}
			}
			return result;
		}

		public static MessageStatusEnum ToMessageStatusEnum(this MessageStatusServiceEnum response)
			=> response switch
			{
				MessageStatusServiceEnum.CREATED => MessageStatusEnum.CREATED,
				MessageStatusServiceEnum.SENT => MessageStatusEnum.SENT,
				MessageStatusServiceEnum.ERROR => MessageStatusEnum.ERROR,
				MessageStatusServiceEnum.EXCEPTION => MessageStatusEnum.EXCEPTION,
				MessageStatusServiceEnum.NOTSENT => MessageStatusEnum.NOTSENT,
				MessageStatusServiceEnum.DELIVERING => MessageStatusEnum.DELIVERING,
				MessageStatusServiceEnum.DELIVERED => MessageStatusEnum.DELIVERED,
				MessageStatusServiceEnum.UNDELIV => MessageStatusEnum.UNDELIV,
				_ => MessageStatusEnum.Undefined
			};
	}
}
