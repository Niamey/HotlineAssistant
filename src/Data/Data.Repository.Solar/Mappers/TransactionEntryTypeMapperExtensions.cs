using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionEntryTypeMapperExtension
	{
		public static EntryTypeEnum ToEntryTypeEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"T" => EntryTypeEnum.Transaction,
				"F" => EntryTypeEnum.Fee,
				"CL" => EntryTypeEnum.Credit_limit,
				_ => EntryTypeEnum.Undefined
			};
	}
}
