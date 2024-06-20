using System;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.Mapper.Cards
{
	internal static class CardKindMapperExtensions
	{
		public static CardKindEnum ToCardStatusEnum(this CardKindRetailEnum response)
				 => response switch
				 {
					 CardKindRetailEnum.DEBITWORLD_BVR => CardKindEnum.DebitWorldBVR,
					 CardKindRetailEnum.MAGSTRIPE_PREDPROD_516818 => CardKindEnum.MagstripePredProd516818,
					 CardKindRetailEnum.MC_MAGSTRIPE => CardKindEnum.MCMagstripe,
					 CardKindRetailEnum.DEBITWORLD_NO_MAGSTRIPE => CardKindEnum.DebitWorldNoMagstripe,
					 _ => CardKindEnum.Undefined
				 };
	}
}
