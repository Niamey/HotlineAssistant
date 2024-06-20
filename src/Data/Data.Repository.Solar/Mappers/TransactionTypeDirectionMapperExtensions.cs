using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionTypeDirectionMapperExtensions
	{
		public static DirectionClassEnum ToDirectionClassEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"DB" => DirectionClassEnum.Debit,
				"CR" => DirectionClassEnum.Credit,
				_ => DirectionClassEnum.Undefined
			};
	}
}
