using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionStatusMapperExtensions
	{
		/*
		public static StmtStatusEnum ToStmtStatusEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"LOADED" => StmtStatusEnum.Loaded,
				"PREPARED" => StmtStatusEnum.Prepared,
				"SUSPENDED" => StmtStatusEnum.Suspended,
				"FINISHED" => StmtStatusEnum.Finished,
				"CLOSED" => StmtStatusEnum.Closed,
				"REJECTED" => StmtStatusEnum.Rejected,
				"CANCELLED" => StmtStatusEnum.Cancelled,
				_ => StmtStatusEnum.Undefined
			};
	
		public static StmtEntryStatusEnum ToStmtEntryStatusEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"RJ" => StmtEntryStatusEnum.Rejected,
				"FD" => StmtEntryStatusEnum.Finished,
				"PP" => StmtEntryStatusEnum.PreProcessing,
				"CD" => StmtEntryStatusEnum.Closed,
				"SH" => StmtEntryStatusEnum.Shaded,
				"PF" => StmtEntryStatusEnum.PreAppliedAsFin,
				_ => StmtEntryStatusEnum.Undefined
			};
		*/
		public static TxnStatusEnum ToTxnStatusEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"NW" => TxnStatusEnum.New,
				"LD" => TxnStatusEnum.Loaded,
				"LE" => TxnStatusEnum.LoadError,
				"PR" => TxnStatusEnum.Prepared,
				"FD" => TxnStatusEnum.Finished,
				"RJ" => TxnStatusEnum.Rejected,
				"CD" => TxnStatusEnum.Closed,
				"PP" => TxnStatusEnum.PreProcessing,
				"SH" => TxnStatusEnum.Shaded,
				"PF" => TxnStatusEnum.PreAppliedAsFin,
				_ => TxnStatusEnum.Undefined
			};		
	}
}
