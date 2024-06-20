using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	public static class CategoryMapperExtensions
	{
		public static CategoryEnum ToCategoryEnum(this CategoryRetailEnum response)
			=> response switch
			{
				CategoryRetailEnum.ADVICE => CategoryEnum.Advice,
				CategoryRetailEnum.REQUEST => CategoryEnum.Request,
				CategoryRetailEnum.NOTIFICATION => CategoryEnum.Notification,
				CategoryRetailEnum.CHECK => CategoryEnum.Check,
				_ => CategoryEnum.Undefined
			};
	}
}
