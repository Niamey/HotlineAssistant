using System;
using Vostok.Hotline.Api.Retail.Responses.Transactions.Enums;
using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Api.Retail.Mapper.Transactions
{
	internal static class TxnStatusMapperExtensions
	{
		public static StmtStatusEnum ToTxnStatusEnum(this TxnStatusRetailEnum response)
		   => response switch
		   {
			   TxnStatusRetailEnum.LOADED => StmtStatusEnum.Loaded,
			   TxnStatusRetailEnum.PREPARED => StmtStatusEnum.Prepared,
			   TxnStatusRetailEnum.SUSPENDED => StmtStatusEnum.Suspended,
			   TxnStatusRetailEnum.FINISHED => StmtStatusEnum.Finished,
			   TxnStatusRetailEnum.CLOSED => StmtStatusEnum.Closed,
			   TxnStatusRetailEnum.REJECTED => StmtStatusEnum.Rejected,
			   TxnStatusRetailEnum.CANCELLED => StmtStatusEnum.Cancelled,
			   _ => StmtStatusEnum.Undefined
		   };
	}
}
