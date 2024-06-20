using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	public static class DirectionMapperExtensions
	{
		public static DirectionEnum ToDirectionEnum(this DirectionRetailEnum response)
			=> response switch
			{
				DirectionRetailEnum.ORIGINAL => DirectionEnum.Original,
				DirectionRetailEnum.ADJUSTMENT => DirectionEnum.Adjustment,
				DirectionRetailEnum.REVERSE => DirectionEnum.Reverse,
				_ => DirectionEnum.Undefined
			};
	}
}
