using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.Models.Cards
{
	public class CardLimitApiModel: ResponseBaseModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string State { get; set; }
		public int OriginalId { get; set; }
		public CardParentLimitApiModel Parent { get; set; }
		public CardLimitValidPeriodApiModel ValidPeriod { get; set; }
		public string LimiterType { get; set; }
		public string Description { get; set; }
		public CardLimitValuesApiModel Values { get; set; }
	}
}
