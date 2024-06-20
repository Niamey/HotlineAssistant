using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	public static class EntryTypeMapperExtensions 
	{
		public static EntryTypeEnum ToEntryTypeEnum(this EntryTypeRetailEnum response)
		   => response switch
		   {
			   EntryTypeRetailEnum.TRANSACTION => EntryTypeEnum.Transaction,
			   EntryTypeRetailEnum.FEE => EntryTypeEnum.Fee,
			   EntryTypeRetailEnum.CREDIT_LIMIT => EntryTypeEnum.Credit_limit,
			   _ => EntryTypeEnum.Undefined
		   };
	}
}