using System.Linq;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardHistoryStatusMapperExtensions
	{
		public static CardCollectionHistoryStatusApiModel ToCardCollectionHistoryStatusApiModel(this CardCollectionHistoryStatusResponseModel response)
		{
			var result = new CardCollectionHistoryStatusApiModel();

			foreach (var row in response.OrderByDescending(x => x.ValidFrom))
			{
				result.Add(row.ToCardHistoryStatusApiModel());
			}

			return result;
		}
		public static CardHistoryStatusApiModel ToCardHistoryStatusApiModel(this CardHistoryStatusResponseModel response)
		{
			var result = new CardHistoryStatusApiModel
			{
				Status = response.Status.ToCardStatusEnum(),
				ModifyDate = response.AuditHistory?.ModifyDate ?? response.ValidFrom,
				Comment = response.AuditHistory?.Comment ?? response.Comment,
				IsEmployee = response.AuditHistory?.IsEmployee,
				SystemName = response.AuditHistory?.SystemName,
				UserLogin = response.AuditHistory?.UserLogin
			};
			return result;
		}
	}
}
