using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	public static class ClassMapperExtensions
	{
		public static ClassEnum ToClassEnum(this ClassRetailEnum response)
		   => response switch
		   {
			   ClassRetailEnum.FINANCIAL => ClassEnum.Financial,
			   ClassRetailEnum.AUTHORIZATION => ClassEnum.Authorization,
			   _ => ClassEnum.Undefined
		   };
	}
}
