using Vostok.Hotline.Core.Common.Enums.Transactions;

namespace Vostok.Hotline.Data.Repository.Solar.Mappers
{
	public static class TransactionCategoryMapperExtensions
	{
		public static CategoryEnum ToCategoryEnum(this string response)
			=> response?.Trim()?.ToUpper() switch
			{
				"A" => CategoryEnum.Advice,
				"R" => CategoryEnum.Request,
				"C" => CategoryEnum.Check,
				_ => CategoryEnum.Undefined
			};
	}
}
