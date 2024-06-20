using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardLimitMapperExtensions
	{
		public static CardLimitApiModel ToCardLimitApiModel(this CardLimitResponseModel response)
		{
			var result = new CardLimitApiModel
			{
				Id = response.Id,
				Code = response.Code,
				State = response.State,
				OriginalId = response.OriginalId,
				Parent = new CardParentLimitApiModel()
				{
					ParentId = response.Parent.ParentId,
					ParentType = response.Parent.ParentType,
					State = response.Parent.State
				},
				ValidPeriod = new CardLimitValidPeriodApiModel()
				{
					ValidFrom = response.ValidPeriod.ValidFrom,
					ValidTo = response.ValidPeriod.ValidTo
				},
				LimiterType = response.LimiterType,
				Description = response.Description,
				Values = new CardLimitValuesApiModel()
				{
					Current = response.Values.Current,
					Remaining = response.Values.Remaining,
					Threshold = new CardLimitThresholdValueApiModel()
					{
						Amount = new CardLimitValueAmountApiModel()
						{
							Amount = response.Values.Threshold.Amount.Amount,
							Currency = response.Values.Threshold.Amount.Currency
						},
						Period = new CardLimitValuePeriodApiModel()
						{
							PeriodType = response.Values.Threshold.Period.PeriodType.ToCardPeriodTypeLimitEnum(),
							Value = response.Values.Threshold.Period.Value
						},
						Count = response.Values.Threshold.Count
					}
				}

			};
			return result;
		}

		public static CardCollectionLimitApiModel ToCardCollectionLimitApiModel(this CardCollectionLimitResponseModel response)
		{
			var result = new CardCollectionLimitApiModel();

			foreach (var row in response)
			{
				result.Add(row.ToCardLimitApiModel());
			}
			return result;
		}


		public static CardPeriodTypeLimitEnum ToCardPeriodTypeLimitEnum(this CardPeriodTypeLimitRetailEnum response)
			=> response switch
			{
				CardPeriodTypeLimitRetailEnum.Day => CardPeriodTypeLimitEnum.Day,
				CardPeriodTypeLimitRetailEnum.Week => CardPeriodTypeLimitEnum.Week,
				CardPeriodTypeLimitRetailEnum.Month => CardPeriodTypeLimitEnum.Month,
				CardPeriodTypeLimitRetailEnum.Year => CardPeriodTypeLimitEnum.Year,
				_ => CardPeriodTypeLimitEnum.Undefined
			};

		public static CardSetLimitApiModel ToCardCardSetLimitApiModel(this CardSetLimitResponseModel response)
		{
			var result = new CardSetLimitApiModel()
			{
				LimitId = response.LimitId
			};
			return result;
		}
	}
}
