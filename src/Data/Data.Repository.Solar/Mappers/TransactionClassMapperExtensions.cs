using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionClassMapperExtensions
	{
		public static ClassEnum ToClassEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"F" => ClassEnum.Financial,
				"A" => ClassEnum.Authorization,
				_ => ClassEnum.Undefined
			};
	}
}
