using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.Responses.Cards
{
	internal class CardLimitResponseModel : ResponseBaseModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string State { get; set; }
		public int OriginalId { get; set; }
		public CardParentLimitResponseModel Parent { get; set; }
		public CardLimitValidPeriodResponseModel ValidPeriod { get; set; }
		public string LimiterType { get; set; }
		public string Description { get; set; }
		public CardLimitValuesResponseModel Values { get; set; }
	}
}
