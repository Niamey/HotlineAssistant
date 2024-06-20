using System;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardTypeMapperExtensions
	{
		public static CardTypeEnum ToCardStatusEnum(this CardTypeRetailEnum response)
					=> response switch
					{
						CardTypeRetailEnum.MASTERCARD_BVR => CardTypeEnum.MasterCardBVR,
						CardTypeRetailEnum.MASTERCARD_BVR_VIRT => CardTypeEnum.MasterCardBVRVirt,
						_ => CardTypeEnum.Undefined
					};
	}
}
