using System;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardCategoryMapperExtensions
	{
		public static CardCategoryEnum ToCardStatusEnum(this CardCategoryRetailEnum response)
			  => response switch
			  {
				  CardCategoryRetailEnum.Primary => CardCategoryEnum.Primary,
				  CardCategoryRetailEnum.Supplementary => CardCategoryEnum.Supplementary,
				  _ => CardCategoryEnum.Undefined
			  };
	}
}
