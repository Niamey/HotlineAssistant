using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionDirectionMapperExtensions
	{
		public static DirectionEnum ToDirectionEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"O" => DirectionEnum.Original,
				"A" => DirectionEnum.Adjustment,
				"R" => DirectionEnum.Reverse,
				_ => DirectionEnum.Undefined
			};
	}
}
