
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vostok.Hotline.Api.Retail.Requests.Commands.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.SearchRequests.Cards
{
	internal class CardChangeStatusCommand
	{
		public CardChangeStatusCommand(long cardId, CardStatusRetailEnum status, CardChangeStatusCommentCommand comment)
		{
			CardId = cardId;
			Status = status;
			Comment = comment;
		}

		public long CardId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public CardStatusRetailEnum Status { get; set; }

		public CardChangeStatusCommentCommand Comment { get; set; }
	}
}
