using Vostok.Hotline.Api.Services.Models.MDES;
using Vostok.Hotline.Api.Services.Responses.MDES;
using Vostok.Hotline.Core.Common.Enums;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class TokenHistoryListMapperExtensions
	{
		public static TokenHistoryApiModel ToTokenHistoryApiModel(this TokenHistoryResponseModel response)
		{
			var result = new TokenHistoryApiModel
			{
				CommentText = response.CommentText,
				Initiator = response.Initiator.ToInitiatorMdesEnum(),
				ReasonCode = response.ReasonCode.ToReasonTypeMdesEnum(),
				StatusCode = response.StatusCode.ToStatusCodeMdesEnum(),
				StatusDateTime = response.StatusDateTime,
				StatusDescription = response.StatusDescription,
				PrevCardId = response.PrevCardId
			};

			if (response.Audit != null)
			{
				result.Audit = new AuditApiModel
				{
					Organization = response.Audit.Organization,
					Phone = response.Audit.Phone,
					UserId = response.Audit.UserId,
					UserName = response.Audit.UserName
				};
			}

			return result;
		}
		public static TokenHistoryCollectionApiModel ToTokenHistoryListApiModel(this TokenHistoryListResponseModel response)
		{
			var result = new TokenHistoryCollectionApiModel();
			foreach (var row in response.Statuses)
			{
				result.Add(row.ToTokenHistoryApiModel());
			}
			return result;
		}
	}
}
