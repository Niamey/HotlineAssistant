using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	public static class DirectionClassMapperExtensions
	{
		public static DirectionClassEnum ToDirectionClassEnum(this DirectionClassRetailEnum response)
			=> response switch
			{
				DirectionClassRetailEnum.DEBIT => DirectionClassEnum.Debit,
				DirectionClassRetailEnum.CREDIT => DirectionClassEnum.Credit,
				_ => DirectionClassEnum.Undefined
			};
	}
}
